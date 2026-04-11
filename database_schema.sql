-- =====================================================
-- DATABASE SCHEMA: FixItNow
-- Platform: SQL Server
-- Generated from EF Core Migration: InitialCreate
-- =====================================================

-- 1. BẢNG THÔNG BÁO
CREATE TABLE [Notifications] (
    [Id]               INT            IDENTITY(1,1) NOT NULL,
    [UserId]           INT            NOT NULL DEFAULT 0,
    [UserPhone]        NVARCHAR(MAX)  NOT NULL,
    [Title]            NVARCHAR(MAX)  NOT NULL,
    [Message]          NVARCHAR(MAX)  NOT NULL,
    [Type]             NVARCHAR(MAX)  NOT NULL,        -- 'new_request', 'accepted', 'rejected', 'completed', 'broadcast_request', 'all_rejected', 'customer_cancelled', 'status_update'
    [RelatedRequestId] INT            NULL,
    [IsRead]           BIT            NOT NULL DEFAULT 0,
    [CreatedAt]        DATETIME2      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id])
);

-- 2. BẢNG YÊU CẦU SỬA CHỮA
CREATE TABLE [RepairRequests] (
    [Id]                INT            IDENTITY(1,1) NOT NULL,
    [CustomerName]      NVARCHAR(MAX)  NOT NULL,
    [CustomerPhone]     NVARCHAR(MAX)  NOT NULL,
    [Address]           NVARCHAR(MAX)  NOT NULL,
    [Category]          NVARCHAR(MAX)  NOT NULL,       -- VD: 'Sửa điện', 'Sửa ống nước'
    [Description]       NVARCHAR(MAX)  NOT NULL,
    [WorkerId]          INT            NULL,            -- NULL khi chưa có thợ nhận (Broadcast/Multi)
    [WorkerName]        NVARCHAR(MAX)  NOT NULL,
    [TargetWorkerIds]   NVARCHAR(MAX)  NULL,            -- CSV format: ',1,2,3,' cho chế độ Multi-select
    [RejectedWorkerIds] NVARCHAR(MAX)  NULL,            -- CSV format: ',1,2,' cho Broadcast đã từ chối
    [IsBroadcast]       BIT            NOT NULL DEFAULT 0,
    [Status]            INT            NOT NULL DEFAULT 0,  -- 0=Pending, 1=Confirmed, 2=Completed, 3=Cancelled
    [CreatedAt]         DATETIME2      NOT NULL DEFAULT GETDATE(),
    [UpdatedAt]         DATETIME2      NULL,
    CONSTRAINT [PK_RepairRequests] PRIMARY KEY ([Id])
);

-- 3. BẢNG ĐÁNH GIÁ
CREATE TABLE [Reviews] (
    [Id]            INT            IDENTITY(1,1) NOT NULL,
    [RequestId]     INT            NOT NULL,
    [CustomerName]  NVARCHAR(MAX)  NOT NULL,
    [CustomerPhone] NVARCHAR(MAX)  NOT NULL,
    [WorkerId]      INT            NOT NULL,
    [WorkerName]    NVARCHAR(MAX)  NOT NULL,
    [Rating]        INT            NOT NULL,            -- 1 đến 5 sao
    [Comment]       NVARCHAR(MAX)  NOT NULL,
    [CreatedAt]     DATETIME2      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id])
);

-- 4. BẢNG NGƯỜI DÙNG
CREATE TABLE [Users] (
    [Id]              INT            IDENTITY(1,1) NOT NULL,
    [FullName]        NVARCHAR(MAX)  NOT NULL,
    [Phone]           NVARCHAR(MAX)  NOT NULL,          -- Số điện thoại (10 chữ số, dùng làm tài khoản đăng nhập)
    [Email]           NVARCHAR(MAX)  NOT NULL DEFAULT '',
    [PasswordHash]    NVARCHAR(MAX)  NOT NULL,
    [Role]            INT            NOT NULL,           -- 0=Customer, 1=Worker
    [WorkerProfileId] INT            NULL,               -- FK tới WorkerProfiles (chỉ có khi Role=Worker)
    [AvatarUrl]       NVARCHAR(MAX)  NULL,
    [CreatedAt]       DATETIME2      NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

-- 5. BẢNG HỒ SƠ THỢ
CREATE TABLE [WorkerProfiles] (
    [Id]          INT            IDENTITY(1,1) NOT NULL,
    [NameOrStore] NVARCHAR(MAX)  NOT NULL,              -- Họ tên hoặc Tên cửa hàng
    [PhoneNumber] NVARCHAR(MAX)  NOT NULL,
    [Address]     NVARCHAR(MAX)  NOT NULL,              -- Địa chỉ chi tiết
    [Description] NVARCHAR(MAX)  NOT NULL DEFAULT '',   -- Mô tả kinh nghiệm
    [Services]    NVARCHAR(MAX)  NOT NULL,              -- JSON array: '["Sửa điện","Sửa nước"]'
    [Location]    NVARCHAR(MAX)  NOT NULL,              -- Khu vực hoạt động: 'Quận 1, Hồ Chí Minh'
    [AvatarUrl]   NVARCHAR(MAX)  NULL,
    [Rating]      FLOAT          NOT NULL DEFAULT 0,
    [IsActive]    BIT            NOT NULL DEFAULT 1,    -- Trạng thái sẵn sàng nhận việc
    CONSTRAINT [PK_WorkerProfiles] PRIMARY KEY ([Id])
);


-- =====================================================
-- SEED DATA: Dữ liệu mẫu
-- =====================================================

-- Seed: 5 tài khoản Thợ + 5 tài khoản Khách (mật khẩu mặc định: 123456)
SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([Id], [FullName], [Phone], [Email], [PasswordHash], [Role], [WorkerProfileId], [AvatarUrl], [CreatedAt])
VALUES
    (101, N'Nguyễn Văn A',    '0900000001', '', '123456', 1, 101, NULL, '2025-01-01'),
    (102, N'Trần Thị B',      '0900000002', '', '123456', 1, 102, NULL, '2025-01-02'),
    (103, N'Phạm C',          '0900000003', '', '123456', 1, 103, NULL, '2025-01-03'),
    (104, N'Lê Văn D',        '0900000004', '', '123456', 1, 104, NULL, '2025-01-04'),
    (105, N'Phạm Văn E',      '0900000005', '', '123456', 1, 105, NULL, '2025-01-05'),
    (106, N'Khách Hàng Một',  '0900000006', '', '123456', 0, NULL, NULL, '2025-01-06'),
    (107, N'Khách Hàng Hai',  '0900000007', '', '123456', 0, NULL, NULL, '2025-01-07'),
    (108, N'Khách Hàng Ba',   '0900000008', '', '123456', 0, NULL, NULL, '2025-01-08'),
    (109, N'Khách Hàng Bốn',  '0900000009', '', '123456', 0, NULL, NULL, '2025-01-09'),
    (110, N'Khách Hàng Năm',  '0900000010', '', '123456', 0, NULL, NULL, '2025-01-10');
SET IDENTITY_INSERT [Users] OFF;

-- Seed: 5 Hồ sơ thợ
SET IDENTITY_INSERT [WorkerProfiles] ON;
INSERT INTO [WorkerProfiles] ([Id], [NameOrStore], [PhoneNumber], [Address], [Description], [Services], [Location], [AvatarUrl], [Rating], [IsActive])
VALUES
    (101, N'Thợ Điện Nguyễn Văn A',     '0900000001', N'123 Lê Lợi, Quận 1',              N'Sửa điện nhanh chóng, an toàn',       N'Sửa điện,Máy lạnh',                        N'Hồ Chí Minh', NULL, 4.8, 1),
    (102, N'Trần Thị B Dịch Vụ Nước',   '0900000002', N'456 Trần Hưng Đạo, Hoàn Kiếm',    N'Chuyên sửa ống nước, vòi sen',        N'Sửa nước',                                  N'Hà Nội',      NULL, 4.5, 1),
    (103, N'Cửa hàng Sửa Chữa C',       '0900000003', N'789 Nguyễn Văn Linh, Hải Châu',   N'Thay lốc tủ lạnh, kiểm tra tivi',    N'Điện gia dụng,Máy lạnh',                   N'Đà Nẵng',     NULL, 5.0, 1),
    (104, N'Lê Văn D - Đa năng',        '0900000004', N'101 Lý Tự Trọng, Ninh Kiều',      N'Nhận sửa mọi thứ trong nhà',         N'Sửa điện,Sửa nước,Điện gia dụng',          N'Cần Thơ',     NULL, 4.2, 1),
    (105, N'Phạm Văn E Mộc',            '0900000005', N'202 Đồng Khởi, Biên Hòa',         N'Chuyên sửa cửa gỗ, tủ gỗ',           N'Khác',                                      N'Đồng Nai',    NULL, 4.7, 1);
SET IDENTITY_INSERT [WorkerProfiles] OFF;
