namespace JwtBearer.Models
{
    public  record User(
        int id,
        string Email,
        string Password,
        string[] Roles);
    
}
