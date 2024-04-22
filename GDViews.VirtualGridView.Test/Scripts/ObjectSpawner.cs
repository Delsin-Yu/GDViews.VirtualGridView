using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GDUtilities;

/// <summary>
/// Performs automatically object pooling when drawing elements,
/// inherit this class to implement customized draw logic in <see cref="DrawElement"/>,
/// and clean up logic in <see cref="CleanupElement"/>.
/// </summary>
/// <typeparam name="TComponent">The node/script type this spawner operates on.</typeparam>
/// <typeparam name="TValue">The value this spawner operates on.</typeparam>
public abstract class ObjectSpawner<TComponent, TValue> where TComponent : Node
{
    private readonly Stack<TComponent> _activeInstance = [];
    private readonly Stack<TComponent> _pooledInstance = [];
    private readonly Control _container;
    private readonly PackedScene _prefab;

    private bool _disposed;

    /// <summary>
    /// Construct an instance of this ObjectSpawner.
    /// </summary>
    /// <param name="container">The container for the instantiated instances when performs drawing.</param>
    /// <param name="prefab">The prefab to instantiate from when performs drawing.</param>
    protected ObjectSpawner(Control container, PackedScene prefab)
    {
        _container = container;
        _prefab = prefab;
    }

    /// <summary>
    /// The amount of the instances that's in use.
    /// </summary>
    public int ActiveCount => _activeInstance.Count;
    
    /// <summary>
    /// The amount of the instances that's pooled.
    /// </summary>
    public int PooledCount => _pooledInstance.Count;

    /// <summary>
    /// Disables all the existing drawn instances,
    /// and create or reuse an instance for each element
    /// inside the supplies collection.
    /// </summary>
    /// <param name="values">The collection to perform
    /// the batch drawing on.</param>
    public void Draw(TValue[] values)
    {
        ThrowIfDisposed();
        CollectedUsedInstance();
        var count = 0;
        foreach (var value in values.AsSpan())
        {
            DrawItem(value, count);
            count++;
        }
    }

    /// <inheritdoc cref="Draw(TValue[])"/>
    public void Draw(List<TValue> values)
    {
        ThrowIfDisposed();
        CollectedUsedInstance();
        var count = 0;
        foreach (var value in CollectionsMarshal.AsSpan(values))
        {
            DrawItem(value, count);
            count++;
        }
    }

    /// <inheritdoc cref="Draw(TValue[])"/>
    public void Draw(IReadOnlyList<TValue> values)
    {
        ThrowIfDisposed();
        CollectedUsedInstance();
        for (var index = 0; index < values.Count; index++)
        {
            var value = values[index];
            DrawItem(value, index);
        }
    }

    /// <inheritdoc cref="Draw(TValue[])"/>
    public void Draw(IEnumerable<TValue> values)
    {
        ThrowIfDisposed();
        CollectedUsedInstance();
        var count = 0;
        foreach (var value in values)
        {
            DrawItem(value, count);
            count++;
        }
    }

    /// <summary>
    /// Remove all the pooled instances from this spawner.
    /// </summary>
    public void Trim()
    {
        while (_pooledInstance.TryPop(out var instance)) 
            instance.Free();
    }

    /// <summary>
    /// Called when the spawner is drawing an instance.
    /// </summary>
    /// <param name="instance">The instance that's getting draw.</param>
    /// <param name="value">The value associated to this instance.</param>
    /// <param name="index">The index for the instance.</param>
    protected abstract void DrawElement(TComponent instance, TValue value, int index);
    
    /// <summary>
    /// Called when the spawner is removing an instance from the container.
    /// </summary>
    /// <param name="instance">The instance that's getting removed from the container.</param>
    protected virtual void CleanupElement(TComponent instance) { }

    /// <inheritdoc/>
    public override string ToString()
    {
        ThrowIfDisposed();

        return
            $"""
             Container: {_container.Name}
             Prefab: {_prefab}
             Active Instance: {ActiveCount}
             Pooled Instance: {PooledCount}
             Total Instance: {ActiveCount + PooledCount}
             """;
    }

    /// <summary>
    /// Remove all associated objects from memory and invalidates this spawner.
    /// Any further attempt to access this instance will result in a run-time error.
    /// </summary>
    public void Free()
    {
        ThrowIfDisposed();

        while (_activeInstance.TryPop(out var instance))
        {
            ResetElement(instance);
            instance.Free();
        }

        while (_pooledInstance.TryPop(out var instance))
        {
            instance.Free();
        }

        _container?.Dispose();
        _prefab?.Dispose(); 
        
        _disposed = true;
    }

    ~ObjectSpawner()
    {
        if(_disposed) return;
        _container?.Dispose();
        _prefab?.Dispose(); 
    }

    private void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException("ObjectSpawner");
    }

    private void CollectedUsedInstance()
    {
        while (_activeInstance.TryPop(out var nodeInstance))
        {
            ResetElement(nodeInstance);
            _container.RemoveChild(nodeInstance);
            _pooledInstance.Push(nodeInstance);
        }
    }

    private void ResetElement(TComponent nodeInstance)
    {
        try
        {
            CleanupElement(nodeInstance);
        }
        catch (Exception e)
        {
            GD.Print(e.ToString());
        }
    }

    private void DrawItem(TValue value, int count)
    {
        if (!_pooledInstance.TryPop(out var nodeInstance))
            nodeInstance = _prefab.Instantiate<TComponent>();

        _container.AddChild(nodeInstance);
        _activeInstance.Push(nodeInstance);

        try
        {
            DrawElement(nodeInstance, value, count);
        }
        catch (Exception e)
        {
            GD.Print(e.ToString());
        }
    }
}