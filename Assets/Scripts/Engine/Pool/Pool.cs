using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool<T>
{
    private List<PoolObject<T>> _poolList; //Lista de PoolObjects que tiene el pool
    public delegate T CallbackFactory(); //Delegate que se va a usar para el factory

    private int _count; //Cantidad de elementos que tiene actualmente, esto es opcional
    private bool _isDinamic = false; //Si el pool es dinámico o no
    private PoolObject<T>.PoolCallback _init; //Método para inicializar
    private PoolObject<T>.PoolCallback _finalize; //Método para finalizar
    private CallbackFactory _factoryMethod; //Referencia al método para factory

    public Pool(int initialStock, CallbackFactory factoryMethod, PoolObject<T>.PoolCallback initialize, PoolObject<T>.PoolCallback finalize, bool isDinamic)
    {
        _poolList = new List<PoolObject<T>>(); //Creo la lista de objetos

        //Guardamos los punteros para cuando los necesitemos.
        _factoryMethod = factoryMethod; //Guardo el factory method
        _isDinamic = isDinamic; //Guardo si es dinámico
        _count = initialStock; //Guardo la cantidad de elementos
        _init = initialize; //Guardo el método para inicializar
        _finalize = finalize; //Guardo el método para finalizar

        //Generamos el stock inicial.
        for (int i = 0; i < _count; i++) //Por cada iteración
        {
            _poolList.Add(new PoolObject<T>(_factoryMethod(), _init, _finalize)); //Creo un pool object y lo agrego a la lista de elementos
        }
    }

    //Método para obtener un pool object (no es necesario, el importante es el de abajo) Igualmente, la lógica de ambos es muy parecida
    public PoolObject<T> GetPoolObject()
    {
        for (int i = 0; i < _count; i++)
        {
            if (!_poolList[i].isActive)
            {
                _poolList[i].isActive = true;
                return _poolList[i];
            }
        }
        if (_isDinamic)
        {
            PoolObject<T> po = new PoolObject<T>(_factoryMethod(), _init, _finalize);
            po.isActive = true;
            _poolList.Add(po);
            _count++;
            return po;
        }
        return null;
    }

    //Método para obtener un objeto del pool
    public T GetObjectFromPool()
    {
        for (int i = 0; i < _count; i++) //Recorro toda la lista de elementos
        {
            if (!_poolList[i].isActive) //Si alguno no esta activo
            {
                _poolList[i].isActive = true; //Lo activo
                return _poolList[i].GetObj; //Y lo devuelvo
            }
        }

        //En caso de que todos esten activos
        if (_isDinamic) //Si el pool es dinámico
        {
            PoolObject<T> po = new PoolObject<T>(_factoryMethod(), _init, _finalize); //Creo un nuevo Pool Object
            po.isActive = true; //Le digo que esta activo
            _poolList.Add(po); //Lo agrego a la lista de objetos
            _count++; //Digo que tengo un nuevo elemento
            return po.GetObj; //Y devuelvo el objeto
        }

        return default(T); //Sino era dinámico y no tenía más elementos, devuelvo el default de T.
    }

    //Método para desactivar un pool object
    public void DisablePoolObject(T obj)
    {
        foreach (PoolObject<T> poolObj in _poolList) //Por cada pool object que tengo en la lista
        {
            if (poolObj.GetObj.Equals(obj)) //Si el objeto que me pasaron es el que guarda el pool object
            {
                poolObj.isActive = false; //Lo desactivo
                return; //Y corto la función.
            }
        }
    }
}
