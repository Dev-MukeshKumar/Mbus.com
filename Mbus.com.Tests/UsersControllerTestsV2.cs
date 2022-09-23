using AutoMapper;
using Mbus.com.Controllers;
using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Helpers.Security.Hashing;
using Mbus.com.Services.Repositories;
using Mbus.com.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Mbus.com.Profiles;
using Mbus.com.Helpers.Parameters;
using Mbus.com.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mbus.com.Tests
{

    public class UsersSeedDataFixture : IDisposable
    {

        public AppDbContext appDbContext { get; private set; }

        public UsersSeedDataFixture()
        {

            DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "UsersMBusDB").Options;
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

    public class UsersControllerTestsV2: IClassFixture<UsersSeedDataFixture>
    {
        UsersSeedDataFixture fixture;

        public UsersControllerTestsV2(UsersSeedDataFixture fixture)
        {
            this.fixture = fixture;
        }

        private AppDbContext GetMockDb()
        {
            //DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "MbusDB").Options;
            //AppDbContext appDbContext = new AppDbContext(dbContextOptions);

            return fixture.appDbContext;
        }

        private UsersController GetController()
        {
            var appDbContext = GetMockDb();
            PasswordHasher passwordHasher = new PasswordHasher();
            IUnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            IBusRepository busRepository = new BusRepository(appDbContext);
            ITicketRepository ticketRepository = new TicketRepository(appDbContext);
            IUserRepository userRepository = new UserRepository(appDbContext);
            IUserServices usersService = new UserServices(userRepository, unitOfWork, passwordHasher, busRepository, ticketRepository);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersProfile());
                cfg.AddProfile(new BusesProfile());
                cfg.AddProfile(new TicketsProfile());
                cfg.AddProfile(new OwnersProfile());
            });

            IMapper mapper = new Mapper(config);
            UsersController userController = new UsersController(usersService, mapper);

            return userController;
        }

        [Fact]
        public async void GetUser_InputEmptyOrNullUserId_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => GetController().GetUser(Guid.Empty));
        }

        [Fact]
        public async void GetUser_InputNotExisitingUserId_ReturnsNoContentResponse()
        {

            var result = await GetController().GetUser(new Guid("30e82623-f675-4f0f-2542-08da90bf8efd"));

            var user = result.Result as NoContentResult;
            Assert.Equal(204, user.StatusCode);
        }

        [Fact]
        public async void GetUser_InputRegisteredUserId_ReturnsOkActionResult()
        {

            Guid userId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d");

            var result = await GetController().GetUser(userId);

            var user = result.Result as OkObjectResult;
            UserDTO value = user.Value as UserDTO;
            Assert.IsType<UserDTO>(user.Value);
            Assert.Equal(200, user.StatusCode);
            Assert.Equal(userId, value.Id);

        }

        [Fact]
        public async void CreateUser_SendValidDataOfUser_ReturnsUserDataWithID()
        {
            UserCreationDTO userData = new UserCreationDTO { Email = "maddy@example.com", Name = "Mukesh", Password = "test@123" };

            var result = await GetController().CreateUser(userData);
            var response = Assert.IsType<CreatedAtRouteResult>(result);
            var userCreated = response.Value as UserDTO;
            Assert.NotNull(userCreated);
            Assert.Equal(userData.Email, userCreated.Email);

            var getUser = await GetController().GetUser(userCreated.Id);
            var getResponse = getUser.Result as OkObjectResult;
            var userInDatabase = getResponse.Value as UserDTO;
            Assert.NotNull(userInDatabase);
            Assert.Equal(userCreated.Email, userInDatabase.Email);
        }

        [Fact]
        public async void CreateUser_SendInValidDataOfUser_ReturnsErrorMessage()
        {
            var result = await GetController().CreateUser(new UserCreationDTO { Email = "max@example.com", Password = "test@123" });
            var response = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = response.Value;
            Assert.Equal("Enter the name, email, and password of the user", errorMessage);
        }

        [Fact]
        public async void CreateUser_ExisitingEmailDataOfUser_ReturnsErrorMessage()
        {
            UserCreationDTO userData = new UserCreationDTO { Email = "mukesh2@example.com", Name = "Mukesh", Password = "test@123" };

            var result = await GetController().CreateUser(userData);
            var response = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = response.Value;
            Assert.Equal("Email already in use.", errorMessage);
        }

        [Fact]
        public void GetAllBus_GiveSomeBasicQueryData_ReturnBusMatchTheQuery()
        {
            var busResourceparameter = new BusResourceParamter { From = "chennai", To = "madurai" };

            var busListResult = GetController().GetAllBus(busResourceparameter);
            var busListResponse = Assert.IsType<OkObjectResult>(busListResult);
            var busList = busListResponse.Value as List<BusToReturnDTO>;

            Assert.NotNull(busList);
            Assert.True(busList.Count >= 0);
            if (busList.Count > 0)
            {
                Assert.Equal(busList[0].From, busResourceparameter.From);
                Assert.Equal(busList[0].To, busResourceparameter.To);
            }
        }

        [Fact]
        public void GetAllBus_BusResourceParameterIsNull_ReturnsbadRequest()
        {

            var busListResult = GetController().GetAllBus(null);
            var busListResponse = Assert.IsType<BadRequestObjectResult>(busListResult);
            var errorMessage = busListResponse.Value;

            Assert.Equal("resourceParamter was null.", errorMessage);
        }

        [Fact]
        public async void GetBusByUserId_UserIdAndBusIdBothEmpty_ReturnsbadRequest()
        {
            var busResult = await GetController().GetBus(Guid.Empty, new Guid("7f188dd5-b786-46bb-a915-de239a0a363d"));
            var busResponse = Assert.IsType<BadRequestObjectResult>(busResult);
            var errorMessage = busResponse.Value;

            Assert.Equal("Enter user id.", errorMessage);

            var bus2Result = await GetController().GetBus(new Guid("30e82623-f675-4f1f-2542-08da90bf8efd"), Guid.Empty);
            var bus2Response = Assert.IsType<BadRequestObjectResult>(bus2Result);
            var error2Message = bus2Response.Value;

            Assert.Equal("Enter bus id.", error2Message);

        }

        [Fact]
        public async void GetBusByUserId_UserIdAndBusIdAreNotRegisteredAndValid_ReturnsNullValue()
        {
            var userId = new Guid("30e82623-f675-4f2f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a905-de239a0a363d");

            var busResult = await GetController().GetBus(userId, busId);
            var busResponse = Assert.IsType<OkObjectResult>(busResult);
            var busData = busResponse.Value as BusToReturnDTO;

            Assert.Null(busData);
        }

        [Fact]
        public async void GetBusByUserId_UserIdAndBusIdAreRegisteredAndValid_ReturnsBusDetails()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var busResult = await GetController().GetBus(userId, busId);
            var busResponse = Assert.IsType<OkObjectResult>(busResult);
            var busData = busResponse.Value as BusToReturnDTO;

            Assert.NotNull(busData);
            Assert.Equal(busId, busData.Id);
        }

        [Fact]
        public async void GetTicketByUserId_UserIdAndTicketIdBothAreEmpty_ReturnsBadRequest()
        {

            var ticketResult = await GetController().GetTicket(new Guid(), Guid.Empty);
            var ticketResponse = Assert.IsType<BadRequestObjectResult>(ticketResult);
            var errorMessage = ticketResponse.Value;

            Assert.Equal("Enter required ids.", errorMessage);

            var ticketResult2 = await GetController().GetTicket(Guid.Empty, new Guid());
            var ticketResponse2 = Assert.IsType<BadRequestObjectResult>(ticketResult2);
            var errorMessage2 = ticketResponse2.Value;

            Assert.Equal("Enter required ids.", errorMessage2);

            var ticketResult3 = await GetController().GetTicket(Guid.Empty, Guid.Empty);
            var ticketResponse3 = Assert.IsType<BadRequestObjectResult>(ticketResult3);
            var errorMessage3 = ticketResponse3.Value;

            Assert.Equal("Enter required ids.", errorMessage3);

        }

        [Fact]
        public async void GetTicketByUserId_UserIdAndTicketIdValidAndRegistered_ReturnsTicketData()
        {
            var userId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d");
            var ticketId = new Guid("967cb60c-9166-4dcb-afbb-070102a14067");

            var ticketResult = await GetController().GetTicket(userId, ticketId);
            var ticketResponse = Assert.IsType<OkObjectResult>(ticketResult);
            var ticketData = ticketResponse.Value as UserTicketToReturnDTO;

            Assert.NotNull(ticketData);
            Assert.Equal(ticketId, ticketData.Id);

        }

        [Fact]
        public async void GetTicketByUserId_UserIdAndTicketIdValidAndNotRegistered_ReturnsNullValue()
        {

            var userId = new Guid("30e82623-f675-4f2f-2542-08da90bf8efd");
            var ticketId = new Guid("1bc3a88d-c7e0-4067-ab7a-fe33f0334327");

            var ticketResult = await GetController().GetTicket(userId, ticketId);
            var ticketResponse = Assert.IsType<BadRequestObjectResult>(ticketResult);
            var errorMessage = ticketResponse.Value;

            Assert.NotNull(errorMessage);
            Assert.Equal("Ticket not booked", errorMessage);
        }

        [Fact]
        public async void BookTicket_InputValidAndRegisteredUserIdAndTicketDetails_ReturnTicketData()
        {
            var userId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d");
            var busId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07");

            var ticketDetails = new TicketCreationDTO { TicketCount = 3, BusId = busId, TravelDate = "19-09-2023" };

            var bookTicketResult = await GetController().BookTicket(userId, ticketDetails);
            var bookTicketResponse = Assert.IsType<CreatedAtRouteResult>(bookTicketResult);
            var bookedTicketData = bookTicketResponse.Value as UserTicketToReturnDTO;

            Assert.NotNull(bookedTicketData);
            Assert.Equal(ticketDetails.TicketCount, bookedTicketData.TicketCount);
            Assert.Equal(ticketDetails.BusId, bookedTicketData.BusId);

            var getTicket = await GetController().GetTicket(userId, bookedTicketData.Id);
            var getTicketResponse = getTicket as OkObjectResult;
            var ticketInDatabase = getTicketResponse.Value as UserTicketToReturnDTO;
            Assert.NotNull(ticketInDatabase);
            Assert.Equal(bookedTicketData.BusId, ticketInDatabase.BusId);
        }

        [Fact]
        public async void BookTicket_InputValidAndNotRegisteredUserId_ReturnBadRequest()
        {
            var userId = new Guid("30e82623-f675-4f3f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketDetails = new TicketCreationDTO { TicketCount = 3, BusId = busId, TravelDate = "13-09-2022" };

            var bookTicketResult = await GetController().BookTicket(userId, ticketDetails);
            var bookTicketResponse = Assert.IsType<BadRequestObjectResult>(bookTicketResult);
            var errorMessage = bookTicketResponse.Value;

            Assert.Equal("Not a valid user", errorMessage);
        }


        [Fact]
        public async void BookTicket_InputValidAndNotRegisteredBusId_ReturnBadRequest()
        {
            var userId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d");
            var busId = new Guid("7f188dd5-b786-46bb-a925-de239a0a363d");

            var ticketDetails = new TicketCreationDTO { TicketCount = 3, BusId = busId, TravelDate = "13-09-2022" };

            var bookTicketResult = await GetController().BookTicket(userId, ticketDetails);
            var bookTicketResponse = Assert.IsType<BadRequestObjectResult>(bookTicketResult);
            var errorMessage = bookTicketResponse.Value;

            Assert.Equal("Bus does not exists.", errorMessage);
        }

        [Fact]
        public async void BookTicket_InputValidAndHalfTicketDetails_ReturnBadRequest()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketDetails = new TicketCreationDTO { BusId = busId, TravelDate = "13-09-2022" };

            var bookTicketResult = await GetController().BookTicket(userId, ticketDetails);
            var bookTicketResponse = Assert.IsType<BadRequestObjectResult>(bookTicketResult);
            var errorMessage = bookTicketResponse.Value;

            Assert.Equal("Give valid ticket booking details.", errorMessage);
        }

        [Fact]
        public async void BookTicket_InputEmptyUserId_ReturnBadRequest()
        {
            var userId = Guid.Empty;
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketDetails = new TicketCreationDTO { TicketCount = 3, BusId = busId, TravelDate = "13-09-2022" };

            var bookTicketResult = await GetController().BookTicket(userId, ticketDetails);
            var bookTicketResponse = Assert.IsType<BadRequestObjectResult>(bookTicketResult);
            var errorMessage = bookTicketResponse.Value;

            Assert.Equal("Enter a valid user id.", errorMessage);
        }

        [Fact]
        public void GetAllTicket_InputValidAndRegisteredUserIdAndQuery_ReturnTicketList()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketResourceParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-09-2022") };

            var ticketListResult = GetController().GetAllTickets(ticketResourceParameter, userId);
            var ticketListResponse = Assert.IsType<OkObjectResult>(ticketListResult);
            var ticketList = ticketListResponse.Value as List<UserTicketToReturnDTO>;

            Assert.NotNull(ticketList);
            Assert.True(ticketList.Count >= 0);
            if (ticketList.Count > 0)
            {
                Assert.Equal(busId, ticketList[0].BusId);
            }
        }

        [Fact]
        public void GetAllTicket_InputEmptyUserId_ReturnBadRequestMessage()
        {
            var userId = Guid.Empty;
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketResourceParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-09-2022") };

            var ticketResult = GetController().GetAllTickets(ticketResourceParameter, userId);
            var ticketResponse = Assert.IsType<BadRequestObjectResult>(ticketResult);
            var errorMessage = ticketResponse.Value;

            Assert.Equal("Enter valid user id.", errorMessage);
        }

        [Fact]
        public void GetAllTicket_InputInvalidQuery_ReturnBadRequestMessage()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var ticketResourceParameter = new TicketResourceParameter { BusId = busId };

            var ticketResult = GetController().GetAllTickets(ticketResourceParameter, userId);
            var ticketResponse = Assert.IsType<BadRequestObjectResult>(ticketResult);
            var errorMessage = ticketResponse.Value;

            Assert.Equal("Enter valid query details.", errorMessage);
        }

        [Fact]
        public async void UpdateUser_InputEmptyUserId_ReturnBadRequestMessage()
        {
            var userId = Guid.Empty;

            var userUpdationDetail = new UserUpdationDTO { Name = "Mukesh" };

            var updateUserResult = await GetController().PartialUpdateUserData(userId, userUpdationDetail);
            var updateUserResponse = Assert.IsType<BadRequestObjectResult>(updateUserResult);
            var errorMessage = updateUserResponse.Value;

            Assert.Equal("Enter valid user id.", errorMessage);
        }

        [Fact]
        public async void UpdateUser_InputInvalidUpdationDetails_ReturnBadRequestMessage()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");

            var updateUserResult = await GetController().PartialUpdateUserData(userId, null);
            var updateUserResponse = Assert.IsType<BadRequestObjectResult>(updateUserResult);
            var errorMessage = updateUserResponse.Value;

            Assert.Equal("Enter valid user updation details.", errorMessage);
        }

        [Fact]
        public async void UpdateUser_InputNotRegisteredUserId_ReturnBadRequestMessage()
        {
            var userId = new Guid("30e82623-f675-4f1f-2541-08da90bf8efd");

            var userUpdationDetail = new UserUpdationDTO();

            var updateUserResult = await GetController().PartialUpdateUserData(userId, userUpdationDetail);
            var updateUserResponse = Assert.IsType<BadRequestObjectResult>(updateUserResult);
            var errorMessage = updateUserResponse.Value;

            Assert.Equal("User does not exists.", errorMessage);
        }

        [Fact]
        public async void UpdateUser_InputValidUpdationDetails_ReturnUpdatedUserData()
        {
            var userId = new Guid("7ef4ae3c-0eea-4f76-9b86-89a66c97822d");

            var userUpdationDetail = new UserUpdationDTO { Name = "Mass Mani" };

            var updateUserResult = await GetController().PartialUpdateUserData(userId, userUpdationDetail);
            var updateUserResponse = Assert.IsType<OkObjectResult>(updateUserResult);
            var updatedUser = updateUserResponse.Value as UserDTO;

            var getUserResult = await GetController().GetUser(userId);
            var getUserResponse = getUserResult.Result as OkObjectResult;
            var userInDatabase = getUserResponse.Value as UserDTO;

            Assert.NotNull(userInDatabase);
            Assert.NotNull(updatedUser);

            Assert.Equal(updatedUser.Id, userInDatabase.Id);
            Assert.Equal(updatedUser.Email, userInDatabase.Email);
            Assert.Equal(updatedUser.Name, userInDatabase.Name);
        }

        [Fact]
        public async void DeleteUser_InputEmptyUserId_ReturnBadRequestMessage()
        {
            var userId = Guid.Empty;

            var deleteUserResult = await GetController().DeleteUser(userId);
            var deleteUserResponse = Assert.IsType<BadRequestObjectResult>(deleteUserResult);
            var errorMessage = deleteUserResponse.Value;

            Assert.Equal("Enter valid user id.", errorMessage);
        }

        [Fact]
        public async void DeleteUser_InputValidNotRegisteredUserId_ReturnBadRequestMessage()
        {
            var userId = new Guid("30e82623-f675-4f1f-2541-08da90bf8efd");

            var deleteUserResult = await GetController().DeleteUser(userId);
            var deleteUserResponse = Assert.IsType<BadRequestObjectResult>(deleteUserResult);
            var errorMessage = deleteUserResponse.Value;

            Assert.Equal("User does not exists.", errorMessage);
        }

        [Fact]
        public async void DeleteUser_InputValidRegisteredUserId_ReturnDeletedUserData()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");

            var deleteUserResult = await GetController().DeleteUser(userId);
            var deleteUserResponse = Assert.IsType<OkObjectResult>(deleteUserResult);
            var deletedUser = deleteUserResponse.Value as UserDTO;

            Assert.NotNull(deletedUser);
            Assert.Equal(userId, deletedUser.Id);
        }

        [Fact]
        public async void DeleteTicket_InputEmptyUserId_ReturnBadRequestMessage()
        {
            var userId = Guid.Empty;
            var ticketId = new Guid("1bc3a88d-c7e0-4167-ab7a-fe33f0334327");

            var deleteTicketResult = await GetController().DeleteTicket(userId, ticketId);
            var deleteTicketResponse = Assert.IsType<BadRequestObjectResult>(deleteTicketResult);
            var errorMessage = deleteTicketResponse.Value;

            Assert.Equal("Enter a valid user id.", errorMessage);
        }


        [Fact]
        public async void DeleteTicket_InputEmptyTicketId_ReturnBadRequestMessage()
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");
            var ticketId = Guid.Empty;

            var deleteTicketResult = await GetController().DeleteTicket(userId, ticketId);
            var deleteTicketResponse = Assert.IsType<BadRequestObjectResult>(deleteTicketResult);
            var errorMessage = deleteTicketResponse.Value;

            Assert.Equal("Enter a valid ticket id.", errorMessage);
        }

        [Theory]
        [InlineData("1bc3a88d-c7e0-4267-ab7a-fe33f0334327")]
        [InlineData("c01200ea-a942-4108-83c3-c35e68ea0669")]

        public async void DeleteTicket_InputNotBookedTicketId_ReturnBadRequestMessage(Guid ticketId)
        {
            var userId = new Guid("30e82623-f675-4f1f-2542-08da90bf8efd");

            var deleteTicketResult = await GetController().DeleteTicket(userId, ticketId);
            var deleteTicketResponse = Assert.IsType<BadRequestObjectResult>(deleteTicketResult);
            var errorMessage = deleteTicketResponse.Value;

            Assert.Equal("Ticket does not exists!", errorMessage);
        }

        [Fact]
        public async void DeleteTicket_InputValidBookedTicketId_ReturnDeletedTicketData()
        {
            var userId = new Guid("4b3c0212-f1eb-4433-bce1-eaaf8a5159c2");
            var ticketId = new Guid("c01200ea-a942-4108-83c3-c35e68ea0669");

            var deleteTicketResult = await GetController().DeleteTicket(userId, ticketId);
            var deleteTicketResponse = Assert.IsType<OkObjectResult>(deleteTicketResult);
            var deletedTicket = deleteTicketResponse.Value as UserTicketToReturnDTO;

            Assert.NotNull(deletedTicket);
            Assert.Equal(ticketId, deletedTicket.Id);
        }
    }
}
