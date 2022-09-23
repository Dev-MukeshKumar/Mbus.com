using AutoMapper;
using CsvHelper;
using Mbus.com.Controllers;
using Mbus.com.Data;
using Mbus.com.Entities;
using Mbus.com.Helpers.Security.Hashing;
using Mbus.com.Services.Repositories;
using Mbus.com.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Mbus.com.Profiles;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Mbus.com.Models;
using Mbus.com.Helpers.Parameters;

namespace Mbus.com.Tests
{
    public class OwnersSeedDataFixture : IDisposable
    {

        public AppDbContext appDbContext { get; private set; }

        public OwnersSeedDataFixture()
        {

            DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "MBusDB").Options;
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

    public class OwnersControllerTestsV2: IClassFixture<OwnersSeedDataFixture>
    {
        OwnersSeedDataFixture fixture;

        public OwnersControllerTestsV2(OwnersSeedDataFixture fixture)
        {
            this.fixture = fixture;
        }

        private AppDbContext GetMockDb() {
            //DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "MbusDB").Options;
            //AppDbContext appDbContext = new AppDbContext(dbContextOptions);

            return fixture.appDbContext;
        }

        private OwnersController GetController()
        {
            var appDbContext = GetMockDb();
            PasswordHasher passwordHasher = new PasswordHasher();
            IUnitOfWork unitOfWork = new UnitOfWork(appDbContext);
            IBusRepository busRepository = new BusRepository(appDbContext);
            ITicketRepository ticketRepository = new TicketRepository(appDbContext);
            IOwnerRepository ownerRepository = new OwnerRepository(appDbContext);
            IOwnerServices ownersService = new OwnerServices(ownerRepository, unitOfWork, passwordHasher, busRepository, ticketRepository);

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UsersProfile());
                cfg.AddProfile(new BusesProfile());
                cfg.AddProfile(new TicketsProfile());
                cfg.AddProfile(new OwnersProfile());
            });

            IMapper mapper = new Mapper(config);
            OwnersController ownerController = new OwnersController(ownersService, mapper);

            return ownerController;
        }

        [Fact]
        public async void GetOwner_InputEmptyOwnerId_ReturnBadRequestMessage()
        {
            var ownerId = Guid.Empty;

            var getOwnerResult = await GetController().GetOwner(ownerId);
            var getOwnerResponse = Assert.IsType<BadRequestObjectResult>(getOwnerResult);
            var errorMessage = getOwnerResponse.Value;

            Assert.Equal("Enter a valid owner id.", errorMessage);
        }

        [Fact]
        public async void GetOwner_InputValidAndNotRegisteredOwnerId_ReturnNullData()
        {
            //new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91")
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e90");

            var getOwnerResult = await GetController().GetOwner(ownerId);
            var getOwnerResponse = Assert.IsType<OkObjectResult>(getOwnerResult);
            var ownerData = getOwnerResponse.Value;

            Assert.Null(ownerData);
        }

        [Fact]
        public async void GetOwner_InputValidAndRegisteredOwnerId_ReturnOwnerData()
        {
            var ownerId = new Guid("69791df3-288f-45bf-919b-bd958d76d55c");

            var getOwnerResult = await GetController().GetOwner(ownerId);
            var getOwnerResponse = Assert.IsType<OkObjectResult>(getOwnerResult);
            var ownerData = getOwnerResponse.Value as OwnerDTO;

            Assert.NotNull(ownerData);
            Assert.Equal(ownerId, ownerData.Id);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", "mass@example.com", "test@123")]
        [InlineData("mass mani", "", "test@123")]
        [InlineData("mass mani", "mass@example.com", "")]

        public async void CreateOwner_InvalidOwnerDetails_ReturnBadRequestMessage(string name, string email, string password)
        {
            var ownerData = new OwnerCreationDTO { Email = email, Password = password, Name = name };

            var createOwnerResult = await GetController().RegisterOwner(ownerData);
            var createOwnerResponse = Assert.IsType<BadRequestObjectResult>(createOwnerResult);
            var errorMessage = createOwnerResponse.Value;

            Assert.Equal("Provide owner details to register.", errorMessage);
        }

        [Fact]
        public async void CreateOwner_ValidOwnerDetails_ReturnOwnerDataWithId()
        {
            var ownerData = new OwnerCreationDTO { Email = "monday@example.com", Password = "test@123", Name = "mass mani" };

            var createOwnerResult = await GetController().RegisterOwner(ownerData);
            var createOwnerResponse = Assert.IsType<CreatedAtRouteResult>(createOwnerResult);
            var createdOwner = createOwnerResponse.Value as OwnerDTO;

            var getOwnerResult = await GetController().GetOwner(createdOwner.Id);
            var getOwnerResponse = Assert.IsType<OkObjectResult>(getOwnerResult);
            var ownerInDatabase = getOwnerResponse.Value as OwnerDTO;

            Assert.NotNull(createdOwner);
            Assert.NotNull(ownerInDatabase);

            Assert.Equal(ownerInDatabase.Email, createdOwner.Email);
            Assert.Equal(ownerInDatabase.Name, createdOwner.Name);
        }

        [Fact]
        public async void CreateOwner_AlreadyRegisteredEmail_ReturnBadRequestMessasge()
        {
            var ownerData = new OwnerCreationDTO { Email = "mani@example.com", Password = "test@123", Name = "mass mani" };

            var createOwnerResult = await GetController().RegisterOwner(ownerData);
            var createOwnerResponse = Assert.IsType<BadRequestObjectResult>(createOwnerResult);
            var errorMessage = createOwnerResponse.Value;

            Assert.Equal("Email already in use.", errorMessage);
        }

        [Theory]
        [InlineData(null, null, null, null, 0, 0)]
        [InlineData("", "chennai", "madurai", "20:00", 40, 500)]
        [InlineData("madurai express", "", "madurai", "20:00", 40, 500)]
        [InlineData("madurai express", "chennai", "", "20:00", 40, 500)]
        [InlineData("madurai express", "chennai", "madurai", "", 40, 500)]
        [InlineData("madurai express", "chennai", "madurai", "20:00", 0, 500)]
        [InlineData("madurai express", "chennai", "madurai", "20:00", 40, 0)]
        public async void CreateBus_InvalidBusDetails_ReturnbadRequestMessage(
            string name, string from, string to, string departureTime, int totalSeats, int ticketPrice)
        {

            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busData = new BusCreationDTO
            {
                Name = name,
                From = from,
                To = to,
                DepartureTime = departureTime,
                TicketPrice = ticketPrice,
                TotalSeats = totalSeats,
            };
            var createBusResult = await GetController().AddBus(ownerId, busData);
            var createBusResponse = Assert.IsType<BadRequestObjectResult>(createBusResult);
            var errorMessage = createBusResponse.Value;

            Assert.Equal("Provide proper bus details.", errorMessage);
        }

        [Fact]
        public async void CreateBus_NotRegisterdOwnerId_ReturnbadRequestMessage()
        {

            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e90");
            var busData = new BusCreationDTO
            {
                Name = "madurai express",
                From = "chennai",
                To = "madurai",
                DepartureTime = "20:00",
                TicketPrice = 400,
                TotalSeats = 40,
            };

            var createBusResult = await GetController().AddBus(ownerId, busData);
            var createBusResponse = Assert.IsType<BadRequestObjectResult>(createBusResult);
            var errorMessage = createBusResponse.Value;

            Assert.Equal("Owner does not exists.", errorMessage);
        }

        [Fact]
        public async void CreateBus_AlreadyExisitngBusName_ReturnbadRequestMessage()
        {

            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busData = new BusCreationDTO
            {
                Name = "mani travels",
                From = "chennai",
                To = "madurai",
                DepartureTime = "20:00",
                TicketPrice = 400,
                TotalSeats = 40,
            };

            var createBusResult = await GetController().AddBus(ownerId, busData);
            var createBusResponse = Assert.IsType<BadRequestObjectResult>(createBusResult);
            var errorMessage = createBusResponse.Value;

            Assert.Equal("Bus already exixts.", errorMessage);
        }

        [Fact]
        public async void CreateBus_InputRegisterdOwnerAndValidBusDetails_ReturnBusDataWithId()
        {

            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busData = new BusCreationDTO
            {
                Name = "madurai express",
                From = "chennai",
                To = "madurai",
                DepartureTime = "20:00",
                TicketPrice = 400,
                TotalSeats = 40,
            };

            var createBusResult = await GetController().AddBus(ownerId, busData);
            var createBusResponse = Assert.IsType<CreatedAtRouteResult>(createBusResult);
            var createdBus = createBusResponse.Value as BusToReturnDTO;

            var getBusResult = await GetController().GetBusById(createdBus.Id);
            var getBusResponse = Assert.IsType<OkObjectResult>(getBusResult);
            var busInDatabase = getBusResponse.Value as BusToReturnDTO;

            Assert.NotNull(busInDatabase);
            Assert.NotNull(createdBus);

            Assert.Equal(busInDatabase.Name, createdBus.Name);
            Assert.Equal(busInDatabase.Id, createdBus.Id);
            Assert.Equal(busInDatabase.Name, createdBus.Name);
            Assert.Equal(busInDatabase.From, createdBus.From);
            Assert.Equal(busInDatabase.To, createdBus.To);
            Assert.Equal(busInDatabase.DepartureTime, createdBus.DepartureTime);
            Assert.Equal(busInDatabase.TicketPrice, createdBus.TicketPrice);
            Assert.Equal(busInDatabase.TotalSeats, createdBus.TotalSeats);
        }

        [Fact]
        public async void GetBus_InputEmptyBusId_ReturnBadRequestMessage()
        {
            var busId = Guid.Empty;

            var getBusResult = await GetController().GetBusById(busId);
            var getBusResponse = Assert.IsType<BadRequestObjectResult>(getBusResult);
            var errorMessage = getBusResponse.Value;

            Assert.Equal("Provide valid bus id.", errorMessage);
        }

        [Fact]
        public async void GetBus_InputNotRegisterdBusId_ReturnNullValue()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a362d");

            var getBusResult = await GetController().GetBusById(busId);
            var getBusResponse = Assert.IsType<OkObjectResult>(getBusResult);
            var busInDatabase = getBusResponse.Value as BusToReturnDTO;

            Assert.Null(busInDatabase);
        }

        [Fact]
        public async void GetBus_InputRegisterdBusId_ReturnBusData()
        {
            var busId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07");

            var getBusResult = await GetController().GetBusById(busId);
            var getBusResponse = Assert.IsType<OkObjectResult>(getBusResult);
            var busInDatabase = getBusResponse.Value as BusToReturnDTO;

            Assert.NotNull(busInDatabase);
            Assert.IsType<BusToReturnDTO>(busInDatabase);
            Assert.Equal(busId, busInDatabase.Id);
        }



        [Fact]
        public async void GetAllBus_GiveSomeBasicQueryData_ReturnBusMatchTheQuery()
        {
            var busResourceparameter = new BusResourceParamter { From = "madurai", To = "chennai" };
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var busListResult = await GetController().GetAllBus(busResourceparameter, ownerId);
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
        public async void GetAllBus_InputNotRegisteredOwnerId_ReturnBadRequestMessage()
        {
            var busResourceparameter = new BusResourceParamter { From = "madurai", To = "chennai" };
            var ownerId = new Guid("1aeeb38a-f6bb-48bf-b1f8-c52afa093e92");

            var busListResult = await GetController().GetAllBus(busResourceparameter, ownerId);
            var busListResponse = Assert.IsType<BadRequestObjectResult>(busListResult);
            var errorMessage = busListResponse.Value;

            Assert.Equal("Invalid owner.", errorMessage);
        }

        [Fact]
        public async void GetAllBus_InputEmptyOwnerId_ReturnBadRequestMessage()
        {
            var busResourceparameter = new BusResourceParamter { From = "madurai", To = "chennai" };
            var ownerId = Guid.Empty;

            var busListResult = await GetController().GetAllBus(busResourceparameter, ownerId);
            var busListResponse = Assert.IsType<BadRequestObjectResult>(busListResult);
            var errorMessage = busListResponse.Value;

            Assert.Equal("Provide proper owner id.", errorMessage);
        }

        [Fact]
        public async void GetAllTickets_RegisteredBusId_ReturnTicketListMatchTheQuery()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-9-2022") };

            var ticketListResult = await GetController().GetAllTickets(ticketParameter, ownerId);
            var ticketListResponse = Assert.IsType<OkObjectResult>(ticketListResult);
            var ticketList = ticketListResponse.Value as List<OwnerTicketToReturnDTO>;

            Assert.NotNull(ticketList);
            Assert.True(ticketList.Count >= 0);
            if (ticketList.Count > 0)
            {
                Assert.Equal(ticketParameter.TravelDate, DateTime.Parse(ticketList[0].TravelDate.Split(" ")[0]));
            }
        }

        [Fact]
        public async void GetAllTickets_InputNotRegisteredBusId_ReturnBadRequestMessage()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a362d");
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-9-2022") };

            var ticketListResult = await GetController().GetAllTickets(ticketParameter, ownerId);
            var ticketListResponse = Assert.IsType<BadRequestObjectResult>(ticketListResult);
            var errorMessage = ticketListResponse.Value;

            Assert.Equal("Bus does not exists.", errorMessage);
        }

        [Fact]
        public async void GetAllTickets_InputInvalidResources_ReturnBadRequestMessage()
        {
            var busId = Guid.Empty;
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-09-2022") };

            var ticketListResult = await GetController().GetAllTickets(ticketParameter, ownerId);
            var ticketListResponse = Assert.IsType<BadRequestObjectResult>(ticketListResult);
            var errorMessage = ticketListResponse.Value;

            Assert.Equal("Provide atleast bus id and travel date.", errorMessage);

            var ticketParameter2 = new TicketResourceParameter { BusId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d"), TravelDate = DateTime.MinValue };

            var ticketListResult2 = await GetController().GetAllTickets(ticketParameter2, ownerId);
            var ticketListResponse2 = Assert.IsType<BadRequestObjectResult>(ticketListResult2);
            var errorMessage2 = ticketListResponse2.Value;
            Assert.Equal("Provide atleast bus id and travel date.", errorMessage2);
        }

        [Fact]
        public async void CheckTicketAvailability_InputInvalidResources_ReturnBadRequestMessage()
        {
            var busId = Guid.Empty;
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-09-2022") };

            var ticketListResult = await GetController().CheckFreeTickets(ticketParameter, ownerId);
            var ticketListResponse = Assert.IsType<BadRequestObjectResult>(ticketListResult);
            var errorMessage = ticketListResponse.Value;

            Assert.Equal("Provide atleast bus id and travel date.", errorMessage);

            var ticketParameter2 = new TicketResourceParameter { BusId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d"), TravelDate = DateTime.MinValue };

            var ticketListResult2 = await GetController().CheckFreeTickets(ticketParameter2, ownerId);
            var ticketListResponse2 = Assert.IsType<BadRequestObjectResult>(ticketListResult2);
            var errorMessage2 = ticketListResponse2.Value;
            Assert.Equal("Provide atleast bus id and travel date.", errorMessage2);
        }

        [Fact]
        public async void CheckTicketAvailability_InputNotRegisteredBusId_ReturnBadRequestMessage()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a362d");
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("15-9-2022") };

            var ticketListResult = await GetController().CheckFreeTickets(ticketParameter, ownerId);
            var ticketListResponse = Assert.IsType<BadRequestObjectResult>(ticketListResult);
            var errorMessage = ticketListResponse.Value;

            Assert.Equal("Bus does not exists.", errorMessage);
        }

        [Fact]
        public async void CheckTicketAvailability_RegisteredBusIdAndPastTravelDate_ReturnBookedMessage()
        {
            var busId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07");
            var ownerId = new Guid("69791df3-288f-45bf-919b-bd958d76d55c");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("12-9-2022") };

            var ticketAvailabilityResult = await GetController().CheckFreeTickets(ticketParameter, ownerId);
            var ticketAvailabilityResultResponse = Assert.IsType<OkObjectResult>(ticketAvailabilityResult);
            var responseMessage = ticketAvailabilityResultResponse.Value;
            var responseList = responseMessage.ToString().Split(" ");
            Assert.Contains("booked", responseList);
        }

        [Fact]
        public async void CheckTicketAvailability_RegisteredBusIdAndFutureTravelDate_ReturnAvailableMessage()
        {
            var busId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07");
            var ownerId = new Guid("69791df3-288f-45bf-919b-bd958d76d55c");
            var ticketParameter = new TicketResourceParameter { BusId = busId, TravelDate = DateTime.Parse("19-9-2022") };

            var ticketAvailabilityResult = await GetController().CheckFreeTickets(ticketParameter, ownerId);
            var ticketAvailabilityResultResponse = Assert.IsType<OkObjectResult>(ticketAvailabilityResult);
            var responseMessage = ticketAvailabilityResultResponse.Value;
            var responseList = responseMessage.ToString().Split(" ");
            Assert.Contains("available", responseList);
        }

        [Fact]
        public async void DeleteOwner_InputEmptyOwnerId_ReturnBadRequestMessage()
        {
            var ownerId = Guid.Empty;

            var deleteOwnerResult = await GetController().DeleteOwner(ownerId);
            var deleteOwnerResponse = Assert.IsType<BadRequestObjectResult>(deleteOwnerResult);
            var errorMessage = deleteOwnerResponse.Value;

            Assert.Equal("Provide proper owner id.", errorMessage);
        }

        [Fact]
        public async void DeleteOwner_InputValidNotRegisteredOwnerId_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("30e82623-f675-4f1f-2541-08da90bf8efd");

            var deleteOwnerResult = await GetController().DeleteOwner(ownerId);
            var deleteOwnerResponse = Assert.IsType<BadRequestObjectResult>(deleteOwnerResult);
            var errorMessage = deleteOwnerResponse.Value;

            Assert.Equal("Owner does not exists.", errorMessage);
        }

        [Fact]
        public async void DeleteOwner_InputValidRegisteredOwnerId_ReturnDeletedOwnerData()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var deleteOwnerResult = await GetController().DeleteOwner(ownerId);
            var deleteOwnerResponse = Assert.IsType<OkObjectResult>(deleteOwnerResult);
            var deletedOwner = deleteOwnerResponse.Value as OwnerDTO;

            Assert.NotNull(deletedOwner);
            Assert.Equal(ownerId, deletedOwner.Id);
        }

        [Fact]
        public async void DeleteBus_InputEmptyBusId_ReturnBadRequestMessage()
        {
            var busId = Guid.Empty;
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var deleteBusResult = await GetController().DeleteBus(ownerId, busId);
            var deleteBusResponse = Assert.IsType<BadRequestObjectResult>(deleteBusResult);
            var errorMessage = deleteBusResponse.Value;

            Assert.Equal("Provide proper bus id.", errorMessage);
        }

        [Fact]
        public async void DeleteBus_InputValidNotRegisteredBusId_ReturnBadRequestMessage()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a362d");
            var ownerId = new Guid("30e82623-f675-4f1f-2541-08da90bf8efd");

            var deleteBusResult = await GetController().DeleteBus(ownerId, busId);
            var deleteBusResponse = Assert.IsType<BadRequestObjectResult>(deleteBusResult);
            var errorMessage = deleteBusResponse.Value;

            Assert.Equal("Bus does not exists.", errorMessage);
        }

        [Fact]
        public async void DeleteBus_InputValidRegisteredBusId_ReturnDeletedBusData()
        {
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var deleteBusResult = await GetController().DeleteBus(ownerId, busId);
            var deleteBusResponse = Assert.IsType<OkObjectResult>(deleteBusResult);
            var deletedBus = deleteBusResponse.Value as BusToReturnDTO;

            Assert.NotNull(deletedBus);
            Assert.Equal(busId, deletedBus.Id);
        }

        [Fact]
        public async void UpdateOwner_InputEmptyOwnerId_ReturnBadRequestMessage()
        {
            var ownerId = Guid.Empty;

            var ownerUpdationDetail = new OwnerUpdationDTO { Name = "Mukesh" };

            var updateOwnerResult = await GetController().PartialUpdateOwnerData(ownerId, ownerUpdationDetail);
            var updateOwnerResponse = Assert.IsType<BadRequestObjectResult>(updateOwnerResult);
            var errorMessage = updateOwnerResponse.Value;

            Assert.Equal("Enter valid owner id.", errorMessage);
        }

        [Fact]
        public async void UpdateOwner_InputInvalidUpdationDetails_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var updateOwnerResult = await GetController().PartialUpdateOwnerData(ownerId, null);
            var updateOwnerResponse = Assert.IsType<BadRequestObjectResult>(updateOwnerResult);
            var errorMessage = updateOwnerResponse.Value;

            Assert.Equal("Enter valid owner updation details.", errorMessage);
        }

        [Fact]
        public async void UpdateOwner_InputNotRegisteredOwnerId_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("30e82623-f675-4f1f-2541-08da90bf8efd");

            var ownerUpdationDetail = new OwnerUpdationDTO();

            var updateOwnerResult = await GetController().PartialUpdateOwnerData(ownerId, ownerUpdationDetail);
            var updateOwnerResponse = Assert.IsType<BadRequestObjectResult>(updateOwnerResult);
            var errorMessage = updateOwnerResponse.Value;

            Assert.Equal("Owner does not exists.", errorMessage);
        }

        [Fact]
        public async void UpdateOwner_InputValidUpdationDetails_ReturnUpdatedOwnerData()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");

            var ownerUpdationDetail = new OwnerUpdationDTO { Name = "Mass Mani" };

            var updateOwnerResult = await GetController().PartialUpdateOwnerData(ownerId, ownerUpdationDetail);
            var updateOwnerResponse = Assert.IsType<OkObjectResult>(updateOwnerResult);
            var updatedOwner = updateOwnerResponse.Value as OwnerDTO;

            var getOwnerResult = await GetController().GetOwner(ownerId);
            var getOwnerResponse = Assert.IsType<OkObjectResult>(getOwnerResult);
            var ownerInDatabase = getOwnerResponse.Value as OwnerDTO;

            Assert.NotNull(ownerInDatabase);
            Assert.NotNull(updatedOwner);

            Assert.Equal(updatedOwner.Id, ownerInDatabase.Id);
            Assert.Equal(updatedOwner.Email, ownerInDatabase.Email);
            Assert.Equal(updatedOwner.Name, ownerInDatabase.Name);
        }

        [Fact]
        public async void UpdateBus_InputEmptyBusId_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busId = Guid.Empty;
            var busUpdationDetails = new BusUpdationDTO();

            var updateBusResult = await GetController().PartialUpdateBusData(ownerId, busId, busUpdationDetails);
            var updateBusResponse = Assert.IsType<BadRequestObjectResult>(updateBusResult);
            var errorMessage = updateBusResponse.Value;

            Assert.Equal("Provide proper bus id.", errorMessage);
        }

        [Fact]
        public async void UpdateBus_InputNotRegisteredBusId_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a362d");
            var busUpdationDetails = new BusUpdationDTO();

            var updateBusResult = await GetController().PartialUpdateBusData(ownerId, busId, busUpdationDetails);
            var updateBusResponse = Assert.IsType<BadRequestObjectResult>(updateBusResult);
            var errorMessage = updateBusResponse.Value;

            Assert.Equal("Bus does not exists.", errorMessage);
        }

        [Fact]
        public async void UpdateBus_InputNullBusData_ReturnBadRequestMessage()
        {
            var ownerId = new Guid("2aeeb38a-f6bb-48bf-b1f8-c52afa093e91");
            var busId = new Guid("7f188dd5-b786-46bb-a915-de239a0a363d");

            var updateBusResult = await GetController().PartialUpdateBusData(ownerId, busId, null);
            var updateBusResponse = Assert.IsType<BadRequestObjectResult>(updateBusResult);
            var errorMessage = updateBusResponse.Value;

            Assert.Equal("Provide valid updation details.", errorMessage);
        }

        [Fact]
        public async void UpdateBus_InputValidBusData_ReturnUpdatedBusData()
        {
            var ownerId = new Guid("69791df3-288f-45bf-919b-bd958d76d55c");
            var busId = new Guid("cec07502-5756-4fc7-8c70-35df12501d07");
            var busUpdationDetails = new BusUpdationDTO { From = "bus stand" };

            var updateBusResult = await GetController().PartialUpdateBusData(ownerId, busId, busUpdationDetails);
            var updateBusResponse = Assert.IsType<OkObjectResult>(updateBusResult);
            var updatedBus = updateBusResponse.Value as BusToReturnDTO;

            var getBusResult = await GetController().GetBusById(busId);
            var getBusResponse = Assert.IsType<OkObjectResult>(getBusResult);
            var busInDatabase = getBusResponse.Value as BusToReturnDTO;

            Assert.NotNull(updatedBus);
            Assert.NotNull(busInDatabase);

            Assert.Equal(busInDatabase.Name, updatedBus.Name);
            Assert.Equal(busInDatabase.From, updatedBus.From);
        }
    }
}
