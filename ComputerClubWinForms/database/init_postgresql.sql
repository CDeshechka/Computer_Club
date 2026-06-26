CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS clients (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(120) NOT NULL,
    phone VARCHAR(30) NOT NULL UNIQUE,
    total_hours NUMERIC(10,2) NOT NULL DEFAULT 0,
    total_spent NUMERIC(12,2) NOT NULL DEFAULT 0,
    discount_percent INTEGER NOT NULL DEFAULT 0
);

CREATE TABLE IF NOT EXISTS computers (
    number INTEGER PRIMARY KEY,
    name VARCHAR(30) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS bookings (
    id SERIAL PRIMARY KEY,
    client_id INTEGER NOT NULL REFERENCES clients(id) ON DELETE RESTRICT,
    computer_number INTEGER NOT NULL REFERENCES computers(number) ON DELETE RESTRICT,
    planned_start TIMESTAMP NOT NULL,
    duration_minutes INTEGER NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'active'
);

CREATE TABLE IF NOT EXISTS sessions (
    id SERIAL PRIMARY KEY,
    client_id INTEGER NOT NULL REFERENCES clients(id) ON DELETE RESTRICT,
    computer_number INTEGER NOT NULL REFERENCES computers(number) ON DELETE RESTRICT,
    start_time TIMESTAMP NOT NULL,
    planned_end_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NULL,
    duration_minutes INTEGER NOT NULL DEFAULT 0,
    total_cost NUMERIC(12,2) NOT NULL DEFAULT 0,
    tariff_per_hour NUMERIC(12,2) NOT NULL DEFAULT 0,
    one_time_discount_percent NUMERIC(5,2) NOT NULL DEFAULT 0,
    personal_discount_percent NUMERIC(5,2) NOT NULL DEFAULT 0,
    is_completed BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE IF NOT EXISTS settings (
    key VARCHAR(60) PRIMARY KEY,
    value VARCHAR(120) NOT NULL
);

CREATE INDEX IF NOT EXISTS idx_bookings_computer_time ON bookings(computer_number, planned_start);
CREATE INDEX IF NOT EXISTS idx_sessions_computer_time ON sessions(computer_number, start_time, planned_end_time);

INSERT INTO users(username, password, role) VALUES
('admin', 'admin123', 'admin'),
('manager', 'manager123', 'manager')
ON CONFLICT (username) DO NOTHING;

INSERT INTO computers(number, name, is_active)
SELECT i, 'ПК ' || i, TRUE FROM generate_series(1, 12) AS s(i)
ON CONFLICT (number) DO NOTHING;

INSERT INTO settings(key, value) VALUES ('TariffPerHour', '150')
ON CONFLICT (key) DO NOTHING;
