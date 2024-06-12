namespace newTestLibrary.Model
{
    public class User
    {
        public int Id { get; set; } 
        public string Name {  get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool isPassRequired {  get; set; }   

        public User()
        {
            
        }
    }
}
