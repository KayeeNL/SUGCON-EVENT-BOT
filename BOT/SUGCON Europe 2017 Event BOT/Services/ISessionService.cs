using System;
using System.Collections.Generic;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    public interface ISessionService
    {
        List<Session> GetSessionsByTopic(string topic);
        List<Session> GetSessionsBySpeaker(int speakerId);
        Session GetSessionInformation(int sessionId);
        List<Session> GetSessionsByRoom(int roomId);
        List<Session> GetSessionsByRoomOnDate(int roomId, DateTime date);
        List<Session> GetSessionsByDate(DateTime date);
    }
}