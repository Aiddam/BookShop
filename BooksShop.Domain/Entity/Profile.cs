using BooksShop.Domain.ViewModel.Profile;
using System.ComponentModel.DataAnnotations;


namespace BooksShop.Domain.Entity
{
    public class Profile
    {
        public int Id { get; set; }

        [Range(0,100)]
        public int? Age { get;set; }
        public byte[]? Avatar { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public Profile()
        {

        }
        public Profile(ProfileViewModel pvm)
        {
            ProfileChanged(pvm);
        }
        public void ProfileChanged(ProfileViewModel pvm)
        {
            Id = pvm.Id;
            Age = pvm.Age;
            Email = pvm.Email;
            User = pvm.User;
            ImageChanged(pvm);
        }

        private void ImageChanged(ProfileViewModel pvm)
        {
            if (pvm.Avatar == null)
            {
                return;
            }
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(pvm.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)pvm.Avatar.Length);
            }
            Avatar = imageData;
        }
        

    }
}
