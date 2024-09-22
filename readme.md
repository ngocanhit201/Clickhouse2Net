
# Clickhouse for window
docker pull clickhouse/clickhouse-server
docker run -d -p 18123:8123 -p19000:9000 --name some-clickhouse-server --ulimit nofile=262144:262144 clickhouse/clickhouse-server
# important package 
# very improtant package from: https://github.com/denis-ivanov/EntityFrameworkCore.ClickHouse
dotnet add package EntityFrameworkCore.ClickHouse
dotnet add package Spectre.Console.Cli

# some necessary package
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design

# Scaffold the entity
My connect string: Host=127.0.0.1;Port=8123;User=default;Password=;Database=db2;Compress=True;CheckCompressedHash=False;SocketTimeout=60000000;Compressor=lz4
dotnet ef dbcontext scaffold "Host=127.0.0.1;Port=8123;User=default;Password=;Database=db2;Compress=True;CheckCompressedHash=False;SocketTimeout=60000000;Compressor=lz4" EntityFrameworkCore.ClickHouse -o Models -c MyDbContext
