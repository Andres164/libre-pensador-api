CREATE USER client_api WITH PASSWORD 'password';

GRANT SELECT, UPDATE ON public."Cards" TO client_api;
GRANT SELECT, DELETE, INSERT ON public."customers" TO client_api;
GRANT SELECT, UPDATE, DELETE, INSERT ON public."employees" TO client_api;
GRANT INSERT ON log_entries TO client_api;
GRANT SELECT ON log_entries TO client_api;
GRANT SELECT, INSERT, UPDATE, DELETE ON public."expenses" TO client_api;
GRANT SELECT, INSERT, UPDATE, DELETE ON public."expense_categories" TO client_api;
