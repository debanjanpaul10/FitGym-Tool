USE [sql-fitgym]
GO
ALTER TABLE [dbo].[MemberDetails] DROP CONSTRAINT [FK_MemberDetails_MembershipStatus]
GO
ALTER TABLE [dbo].[FeesStatus] DROP CONSTRAINT [FK_FeesStatus_MemberDetails]
GO
ALTER TABLE [dbo].[FeesStatus] DROP CONSTRAINT [FK_FeesStatus_FeesPaymentStatusMapping]
GO
ALTER TABLE [dbo].[FeesStatus] DROP CONSTRAINT [FK_FeesStatus_FeesDurationMapping]
GO
ALTER TABLE [dbo].[FeesPaymentHistory] DROP CONSTRAINT [FK_FeesPaymentHistory_MemberDetails_MI]
GO
ALTER TABLE [dbo].[FeesPaymentHistory] DROP CONSTRAINT [FK_FeesPaymentHistory_FeesPaymentStatusMapping]
GO
ALTER TABLE [dbo].[FeesPaymentHistory] DROP CONSTRAINT [FK_FeesPaymentHistory_FeesDurationMapping]
GO
/****** Object:  Table [dbo].[MembershipStatusMapping]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MembershipStatusMapping]') AND type in (N'U'))
DROP TABLE [dbo].[MembershipStatusMapping]
GO
/****** Object:  Table [dbo].[MemberDetails]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberDetails]') AND type in (N'U'))
DROP TABLE [dbo].[MemberDetails]
GO
/****** Object:  Table [dbo].[FeesStatus]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeesStatus]') AND type in (N'U'))
DROP TABLE [dbo].[FeesStatus]
GO
/****** Object:  Table [dbo].[FeesPaymentStatusMapping]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeesPaymentStatusMapping]') AND type in (N'U'))
DROP TABLE [dbo].[FeesPaymentStatusMapping]
GO
/****** Object:  Table [dbo].[FeesPaymentHistory]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeesPaymentHistory]') AND type in (N'U'))
DROP TABLE [dbo].[FeesPaymentHistory]
GO
/****** Object:  Table [dbo].[FeesDurationMapping]    Script Date: 13-07-2025 17:35:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FeesDurationMapping]') AND type in (N'U'))
DROP TABLE [dbo].[FeesDurationMapping]
GO
