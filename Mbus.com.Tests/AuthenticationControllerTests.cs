using AutoMapper;
using Mbus.com.Controllers;
using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Services.Repositories;
using Mbus.com.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Mbus.com.Helpers.Security.Hashing;
using Mbus.com.Profiles;
using Xunit;
using Mbus.com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace Mbus.com.Tests
{
    public class AuthenticationSeedDataFixture: IDisposable
    {
        public AppDbContext appDbContext { get; private set; }

        public AuthenticationSeedDataFixture()
        {
            DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "AuthenticationMBusDB").Options;
            appDbContext = new AppDbContext(dbContextOptions);


            PasswordHasher passwordHasher = new PasswordHasher();

            appDbContext.Database.EnsureCreated();

            #region Seeding user data
            var users = new List<User> {
                    new User
                    {
                        Id = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd"),
                        Name = "mukesh",
                        Email = "mukesh@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                        TicketsBooked = new List<Ticket>()
                    },
                    new User
                    {
                        Id = new Guid("4b3c0212-f1eb-4433-bce1-eaaf8a5159c2"),
                        Name = "mukesh1",
                        Email = "mukesh1@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                        TicketsBooked = new List<Ticket>()
                    },
                    new User
                    {
                        Id = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d"),
                        Name = "mukesh2",
                        Email = "mukesh2@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                        TicketsBooked = new List<Ticket>()
                    }
            };
            appDbContext.Users.AddRange(users);
            #endregion

            #region Seeding owner data
            var owners = new List<Owner> {
                    new Owner
                    {
                        Id = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91"),
                        Name = "mani",
                        Email = "mani@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                    },
                    new Owner
                    {
                        Id = new Guid("6abd657c-8841-4da1-a4ec-c95b5ceab959"),
                        Name = "mani1",
                        Email = "mani1@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                    },
                    new Owner
                    {
                        Id = new Guid("69791df3-288f-45bf-919b-bd958d76d55c"),
                        Name = "mani2",
                        Email = "mani2@example.com",
                        Password = passwordHasher.HashPassword("test@123"),
                    }
            };
            appDbContext.Owners.AddRange(owners);
            #endregion

            #region Seeding bus data

            var buses = new List<Bus> {
                new Bus {
                    Id = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d"),
                    Name = "madras travels",
                    From = "madurai",
                    To = "chennai",
                    TotalSeats = 40,
                    TicketPrice = 500,
                    DepartureTime = DateTime.Parse("19:30"),
                    OwnerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91"),
                },
                new Bus {
                    Id = new Guid("9fcfadfc-19bb-49fe-aa46-ae47d228294e"),
                    Name = "madurai travels",
                    From = "chennai",
                    To = "madurai",
                    TotalSeats = 40,
                    TicketPrice = 500,
                    DepartureTime = DateTime.Parse("20:30"),
                    OwnerId = new Guid("6abd657c-8841-4da1-a4ec-c95b5ceab959"),
                },
                new Bus {
                    Id = new Guid("cec07502-5756-4fc7-8c70-35df12501d07"),
                    Name = "mani travels",
                    From = "chennai",
                    To = "kanyakumari",
                    TotalSeats = 40,
                    TicketPrice = 800,
                    DepartureTime = DateTime.Parse("21:15"),
                    OwnerId = new Guid("69791df3-288f-45bf-919b-bd958d76d55c"),
                },
            };

            appDbContext.Buses.AddRange(buses);
            #endregion

            #region Seeding ticket data

            var tickets = new List<Ticket> {
                new Ticket{
                    Id = new Guid("1bc3a88d-c7e0-4167-ab7a-fe33f0334327"),
                    BusName = buses[0].Name,
                    UserName = users[0].Name,
                    BookedDate = DateTime.Parse("12-09-2022"),
                    TravelDate = DateTime.Parse("15-09-2022"),
                    TicketCount = 3,
                    TotalPrice = buses[0].TicketPrice*3,
                    UserId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd"),
                    BusId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d"),
                },
                new Ticket{
                    Id = new Guid("c01200ea-a942-4108-83c3-c35e68ea0669"),
                    BusName = buses[1].Name,
                    UserName = users[1].Name,
                    BookedDate = DateTime.Parse("12-09-2022"),
                    TravelDate = DateTime.Parse("15-09-2022"),
                    TicketCount = 1,
                    TotalPrice = buses[1].TicketPrice*1,
                    UserId = new Guid("4b3c0212-f1eb-4433-bce1-eaaf8a5159c2"),
                    BusId = new Guid("9fcfadfc-19bb-49fe-aa46-ae47d228294e"),
                },
                new Ticket{
                    Id = new Guid("967cb60c-9166-4dcb-afbb-070102a14067"),
                    BusName = buses[2].Name,
                    UserName = users[2].Name,
                    BookedDate = DateTime.Parse("12-09-2022"),
                    TravelDate = DateTime.Parse("15-09-2022"),
                    TicketCount = 2,
                    TotalPrice = buses[2].TicketPrice*2,
                    UserId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d"),
                    BusId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07"),
                }
            };

            appDbContext.Tickets.AddRange(tickets);

            #endregion

            appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            appDbContext.Database.EnsureDeleted();
            appDbContext.Dispose();
        }
    }
    public class AuthenticationControllerTests: IClassFixture<AuthenticationSeedDataFixture>
    {

        AuthenticationSeedDataFixture fixture;

        public AuthenticationControllerTests(AuthenticationSeedDataFixture fixture)
        {
            this.fixture = fixture;
        }

        private AppDbContext GetMockDB()
        {
            return fixture.appDbContext;
        }

        private AuthenticationController GetController()
        {
            var appDbContext = GetMockDB();

            IOptions<AppSettings> appSettings = Options.Create(new AppSettings
            {
                ExpirationInMinutes = "1440",
                Secret = "this is a secret"
            });

            PasswordHasher passwordHasher = new PasswordHasher();
            UnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            BusRepository busRepository = new BusRepository(appDbContext);
            TicketRepository ticketRepository = new TicketRepository(appDbContext);
            UserRepository userRepository = new UserRepository(appDbContext);
            OwnerRepository ownerRepository = new OwnerRepository(appDbContext);
            UserServices usersService = new UserServices(userRepository, unitOfWork, passwordHasher, busRepository, ticketRepository);
            OwnerServices ownersService = new OwnerServices(ownerRepository, unitOfWork, passwordHasher, busRepository, ticketRepository);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersProfile());
                cfg.AddProfile(new BusesProfile());
                cfg.AddProfile(new TicketsProfile());
                cfg.AddProfile(new OwnersProfile());
            });
            IMapper mapper = new Mapper(config);

            AuthenticateService authenticateService = new AuthenticateService(usersService, appSettings, ownersService, mapper);
            AuthenticationController authenticationController = new AuthenticationController(authenticateService, mapper);

            return authenticationController;
        }


        [Fact]
        public async void UserLogin_InputValidAndRegistered_ReturnsUserDataWithJWToken()
        {
            var userLoginData = new UserLoginDTO { Email= "mukesh@example.com", Password="test@123" };

            var userLoginResult = await GetController().UserAuthentication(userLoginData);
            var userLoginResponse = Assert.IsType<OkObjectResult>(userLoginResult);
            var userLogin = userLoginResponse.Value as UserTokenDTO;

            Assert.NotNull(userLogin);
            Assert.Equal(userLoginData.Email, userLogin.Email);
            Assert.NotNull(userLogin.Token);
        }

        [Theory]
        [InlineData("muki@example.com", "test@123")]
        [InlineData("mukesh@example.com", "test@12")]
        public async void UserLogin_InputNotValidAndNotRegisteredUser_ReturnsBadResponseMessage(string email,string password)
        {
            var userLoginData = new UserLoginDTO { Email = email, Password = password};

            var userLoginResult = await GetController().UserAuthentication(userLoginData);
            var userLoginResponse = Assert.IsType<BadRequestObjectResult>(userLoginResult);
            var errorMessage = userLoginResponse.Value as Object;

            Assert.Equal("{ message = email or password is incorrect }", errorMessage.ToString());
        }

        [Fact]
        public async void OwnerLogin_InputValidAndRegistered_ReturnsOwnerDataWithJWToken()
        {
            var ownerLoginData = new OwnerLoginDTO { Email = "mani@example.com", Password = "test@123" };

            var ownerLoginResult = await GetController().OwnerAuthentication(ownerLoginData);
            var ownerLoginResponse = Assert.IsType<OkObjectResult>(ownerLoginResult);
            var ownerLogin = ownerLoginResponse.Value as OwnerTokenDTO;

            Assert.NotNull(ownerLogin);
            Assert.Equal(ownerLoginData.Email, ownerLogin.Email);
            Assert.NotNull(ownerLogin.Token);
        }

        [Theory]
        [InlineData("man@example.com", "test@123")]
        [InlineData("mani@example.com", "test@12")]
        public async void OwnerLogin_InputNotValidAndNotRegisteredOwner_ReturnsBadResponseMessage(string email, string password)
        {
            var ownerLoginData = new OwnerLoginDTO { Email = email, Password = password };

            var ownerLoginResult = await GetController().OwnerAuthentication(ownerLoginData);
            var ownerLoginResponse = Assert.IsType<BadRequestObjectResult>(ownerLoginResult);
            var errorMessage = ownerLoginResponse.Value as Object;

            Assert.Equal("{ message = email or password is incorrect }", errorMessage.ToString());
        }
    }
}
