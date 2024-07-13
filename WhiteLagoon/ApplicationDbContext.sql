CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedAt] date NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Villas] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] float NOT NULL,
    [Sqft] int NOT NULL,
    [Occupancy] int NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [Created_Date] datetime2 NULL,
    [Updated_Date] datetime2 NULL,
    CONSTRAINT [PK_Villas] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Amenities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [VillaId] int NOT NULL,
    CONSTRAINT [PK_Amenities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Amenities_Villas_VillaId] FOREIGN KEY ([VillaId]) REFERENCES [Villas] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [Bookings] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [VillaId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NULL,
    [TotalCost] float NOT NULL,
    [Nights] int NOT NULL,
    [Status] nvarchar(max) NULL,
    [BookingDate] datetime2 NOT NULL,
    [CheckInDate] date NOT NULL,
    [CheckOutDate] date NOT NULL,
    [IsPaymentSuccessful] bit NOT NULL,
    [PaymentDate] datetime2 NOT NULL,
    [StripeSessionId] nvarchar(max) NULL,
    [StripePaymentIntentId] nvarchar(max) NULL,
    [ActualCheckInDate] datetime2 NOT NULL,
    [ActualCheckOutDate] datetime2 NOT NULL,
    [VillaNumber] int NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Villas_VillaId] FOREIGN KEY ([VillaId]) REFERENCES [Villas] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [VillaNumbers] (
    [Villa_Number] int NOT NULL,
    [VillaId] int NOT NULL,
    [SpecialDetails] nvarchar(max) NULL,
    CONSTRAINT [PK_VillaNumbers] PRIMARY KEY ([Villa_Number]),
    CONSTRAINT [FK_VillaNumbers_Villas_VillaId] FOREIGN KEY ([VillaId]) REFERENCES [Villas] ([Id]) ON DELETE CASCADE
);
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Created_Date', N'Description', N'ImageUrl', N'Name', N'Occupancy', N'Price', N'Sqft', N'Updated_Date') AND [object_id] = OBJECT_ID(N'[Villas]'))
    SET IDENTITY_INSERT [Villas] ON;
INSERT INTO [Villas] ([Id], [Created_Date], [Description], [ImageUrl], [Name], [Occupancy], [Price], [Sqft], [Updated_Date])
VALUES (1, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x400', N'Royal Villa', 4, 200.0E0, 550, NULL),
(2, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x401', N'Premium Pool Villa', 4, 300.0E0, 550, NULL),
(3, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x402', N'Luxury Pool Villa', 4, 400.0E0, 750, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Created_Date', N'Description', N'ImageUrl', N'Name', N'Occupancy', N'Price', N'Sqft', N'Updated_Date') AND [object_id] = OBJECT_ID(N'[Villas]'))
    SET IDENTITY_INSERT [Villas] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'VillaId') AND [object_id] = OBJECT_ID(N'[Amenities]'))
    SET IDENTITY_INSERT [Amenities] ON;
INSERT INTO [Amenities] ([Id], [Description], [Name], [VillaId])
VALUES (1, N'B', N'A', 1),
(2, N'BB', N'AA', 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'VillaId') AND [object_id] = OBJECT_ID(N'[Amenities]'))
    SET IDENTITY_INSERT [Amenities] OFF;
GO


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Villa_Number', N'SpecialDetails', N'VillaId') AND [object_id] = OBJECT_ID(N'[VillaNumbers]'))
    SET IDENTITY_INSERT [VillaNumbers] ON;
INSERT INTO [VillaNumbers] ([Villa_Number], [SpecialDetails], [VillaId])
VALUES (101, NULL, 1),
(102, NULL, 1),
(103, NULL, 1),
(104, NULL, 1),
(201, NULL, 2),
(202, NULL, 2),
(301, NULL, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Villa_Number', N'SpecialDetails', N'VillaId') AND [object_id] = OBJECT_ID(N'[VillaNumbers]'))
    SET IDENTITY_INSERT [VillaNumbers] OFF;
GO


CREATE INDEX [IX_Amenities_VillaId] ON [Amenities] ([VillaId]);
GO


CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO


CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO


CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO


CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO


CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO


CREATE INDEX [IX_Bookings_UserId] ON [Bookings] ([UserId]);
GO


CREATE INDEX [IX_Bookings_VillaId] ON [Bookings] ([VillaId]);
GO


CREATE INDEX [IX_VillaNumbers_VillaId] ON [VillaNumbers] ([VillaId]);
GO