// See https://aka.ms/new-console-template for more information

using Mapster;
using ScratchPad;

var user = new User(Email: "grimmersnee@gmail.com", FirstName: "Sam", LastName: "Grimmer");

var userDto = user.Adapt<UserDto>();

Console.WriteLine(user);

Console.WriteLine(userDto);

