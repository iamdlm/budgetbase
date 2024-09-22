START TRANSACTION;

ALTER TABLE "AspNetUsers" ADD "ThemeOpt" text NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240921182735_AddUserThemeOpt', '7.0.5');

COMMIT;

