CREATE TABLE "Logs" (
    "Id" SERIAL PRIMARY KEY,
    "Date" TIMESTAMPTZ NOT NULL,
	"LogLevel" TEXT NOT NULL,
    "ThreadId" INT NOT NULL,
    "RequestId" TEXT NOT NULL,
    "EventId" INT NOT NULL,
    "EventName" TEXT NOT NULL,
    "ExceptionMessage" TEXT,
    "ExceptionStackTrace" TEXT,
    "ExceptionSource" TEXT
);