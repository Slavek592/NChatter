using NChatterServer.Core.Models;
using NChatterServer.Data.Models;

namespace NChatterServer.Data
{
    public class Mapping
    {
        public static UserModel UserToModel(User user)
        {
            UserModel userModel = new UserModel();
            userModel.Id = user.Id;
            userModel.Password = user.Password;
            userModel.UserName = user.UserName;
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
            messageModel.Status = message.Status;
            messageModel.SentFrom = message.SentFrom;
            messageModel.SentTime = message.SentTime;
            return messageModel;
        }
    }
}