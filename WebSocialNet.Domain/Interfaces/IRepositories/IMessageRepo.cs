using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocialNet.Domain.DTOs.MessageDTOs;
using WebSocialNet.Domain.Entities;

namespace WebSocialNet.Domain.Interfaces.IRepositories
{
    public interface IMessageRepo
    {
        // Save changes (?)
        public void SaveChanges();

        // Create
        public Message Add(Message thisMessage);

        // Read
        public IEnumerable<MessageDTO> GetMessagesFromChatId(string chatId);

        public Message GetById(string _id);

        // Delete
        public void Delete(string _id);
    }
}
