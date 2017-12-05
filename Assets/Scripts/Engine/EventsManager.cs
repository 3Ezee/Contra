using System.Collections.Generic;

public class EventsManager
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<EventType, EventReceiver> _events;

    /// <summary>
    /// Llamamos a este método para suscribirnos a eventos
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void SubscribeToEvent(EventType eventType, EventReceiver listener)
    {
        if (_events == null) //Si el diccionario aún no existe
            _events = new Dictionary<EventType, EventReceiver>(); //Lo creo

        if (!_events.ContainsKey(eventType)) //Si el diccionario no contiene la key
            _events.Add(eventType, null); //Agrego la key

        _events[eventType] += listener; //Agrego la función a esa key
    }

    /// <summary>
    /// Llamamos a este método para desuscribirnos de eventos
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public static void UnsubscribeToEvent(EventType eventType, EventReceiver listener)
    {
        if (_events != null) //Si el diccionario existe
        {
            if (_events.ContainsKey(eventType)) //Si contiene la key que me pasaron
                _events[eventType] -= listener; //Remuevo la función de esa key
        }
    }

    /// <summary>
    /// Llamamos a esta función para disparar un evento
    /// </summary>
    /// <param name="eventType"></param>
    public static void TriggerEvent(EventType eventType)
    {
        TriggerEvent(eventType, null); //Disparo el evento sin params
    }

    /// <summary>
    /// Dispara el evento
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="parametersWrapper"></param>
    public static void TriggerEvent(EventType eventType, params object[] parametersWrapper)
    {
        if (_events == null) //Si el diccionario no existe aún
        {
            UnityEngine.Debug.LogWarning("No events subscribed"); //Tiro un warning de que nadie se subscribio a nada aún
            return; //Y corto
        }

        if (_events.ContainsKey(eventType)) //Si la key del evento
        {
            if (_events[eventType] != null) //Y el delegado no es null
                _events[eventType](parametersWrapper); //Llamo a su función y le paso los params
        }
    }
}

/// <summary>
/// Lista de eventos posibles
/// </summary>
public enum EventType
{
    GP_ShootProjectile,
    GP_Life,
    GP_Win,
    GP_Loose,
    GP_Score,
    GP_Destroy
}