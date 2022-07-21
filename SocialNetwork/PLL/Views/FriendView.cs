using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class FriendView
    {
        UserService userService;
        FriendService friendService;
        public FriendView(FriendService friendService, UserService userService)
        {
            this.friendService = friendService;
            this.userService = userService;
        }
        public void Show(User user)
        {
            var friendData = new FriendData();

            Console.Write("Введите почтовый адрес кого добавить в друзья: ");
            friendData.FriendEmail = Console.ReadLine();

            friendData.UserId = user.Id;
            try
            {
                friendService.AddFriend(friendData);

                SuccessMessage.Show("Пользователь добавлен в друзья!");

                user = userService.FindById(user.Id);
            }

            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }

            catch (ArgumentNullException)
            {
                AlertMessage.Show("Введите корректное значение!");
            }
            catch (FriendExistException)
            {
                AlertMessage.Show("Пользователь уже в друзьях!");
            }
        }
    }
}