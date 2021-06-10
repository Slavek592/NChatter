using NChatterServer.Core.Models;
using NChatterServer.Data.Models;
using System;

namespace NChatterServer.Data
{
    public class Mapping
    {
        public static UserModel UserToModel(User user)
        {
            UserModel userModel = new UserModel();
            userModel.Id = user.Id;
            userModel.Username = user.Username;
            return userModel;
        }
        public static GroupModel GroupToModel(Group group)
        {
            GroupModel groupModel = new GroupModel();
            groupModel.Id = group.Id;
            groupModel.Name = group.Name;
            return groupModel;
        }
        public static ContactModel ContactToModel(Contact contact)
        {
            ContactModel contactModel = new ContactModel();
            contactModel.Id = contact.Id;
            contactModel.Name = contact.Name;
            return contactModel;
        }

        public static MessageModel MessageToModel(Message message)
        {
            MessageModel messageModel = new MessageModel();
            messageModel.Id = message.Id;
            messageModel.Text = message.Text;
            if (message.Status == MessageStatus.Status.NotSent)
            {
                messageModel.Status = "Not sent";
            }
            else if (message.Status == MessageStatus.Status.Sent)
            {
                messageModel.Status = "Sent";
            }
            else if (message.Status == MessageStatus.Status.Delivered)
            {
                messageModel.Status = "Delivered";
            }
            else
            {
                messageModel.Status = "Read";
            }
            messageModel.SentFrom = message.SentFrom;
            messageModel.SentTime = message.SentTime.ToString("r",
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            return messageModel;
        }
        
        public static Message ModelToMessage(MessageModel messageModel)
        {
            Message message = new Message();
            message.Id = messageModel.Id;
            message.Text = messageModel.Text;
            return message;
        }
    }
}