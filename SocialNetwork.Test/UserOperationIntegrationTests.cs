using NUnit.Framework;
using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetwork.Test
{
    [TestFixture]    
    public class UserOperationIntegrationTests
    {
        [Test]
        public void SaveUserOperationMustCreateUserOperationInBase()
        {
            var userTest = new UserOperation();
            var userEntity = new UserEntity { id = 3, firstname = "Сидор", lastname = "Сидоров", password = "10101010", email = "sidorov@mail.ru", photo = "", favorite_book = "", favorite_movie = "" };

            int userId = userTest.Create(userEntity);
            var allUsersAfterAddingNewUser = userTest.ListAll();
            CollectionAssert.Contains(allUsersAfterAddingNewUser, userEntity);

            var Id = userTest.DeleteById(userEntity.id);

            var allUsersAfterDeletingNewUser = userTest.ListAll();
            CollectionAssert.DoesNotContain(allUsersAfterDeletingNewUser, userEntity);
        }
    }
}
