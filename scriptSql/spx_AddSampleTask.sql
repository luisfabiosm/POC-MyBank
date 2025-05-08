SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE spx_AddSampleTask
 @pName              VARCHAR(100)
,@pIntervalSeconds   INTEGER
,@pIsEnabled         BIT
,@pId                INTEGER = 0 OUTPUT
AS
BEGIN
  	
	INSERT INTO dbo.Tasks (Name, IsTimer, TimerOnMiliseconds)
    VALUES (@pName, @pIsEnabled, @pIntervalSeconds);

    -- Captura o ID da linha recém inserida
    SET @pId = SCOPE_IDENTITY();
END
GO
