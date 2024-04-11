CREATE TABLE entity (
    code INTEGER NOT NULL PRIMARY KEY,
    name TEXT NOT NULL,
    meaning TEXT NOT NULL,
    created_at TEXT NOT NULL DEFAULT current_timestamp,
    delete_state_code INTEGER NOT NULL DEFAULT 0
);
INSERT INTO entity (code, name, meaning)
VALUES ('0', 'complate_state', 'Новая'),
	   ('1', 'complate_state', 'Не начата'),
	   ('2', 'complate_state', 'В работе'),
	   ('3', 'complate_state', 'Завершено');
CREATE TABLE groups (
    group_id TEXT NO NULL PRIMARY KEY,
    name TEXT NO NULL,
    e_complate_state INTEGER NOT NULL DEFAULT 0,
    complate_percent INTEGER NOT NULL DEFAULT 0 CHECK(complate_percent>=0 AND complate_percent<=100),
    created_at TEXT NOT NULL DEFAULT current_timestamp,
    modified_at TEXT NOT NULL,
    delete_state_code INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (e_complate_state) REFERENCES entity (code)
);
CREATE TABLE tests (
    test_id TEXT NOT NULL PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT NOT NULL,
    e_complate_state INTEGER NOT NULL DEFAULT 0,
    check_script BLOB NOT NULL,
    created_at TEXT NOT NULL DEFAULT current_timestamp,
    modified_at TEXT NULL,
    delete_state_code INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (e_complate_state) REFERENCES entity (code)
);
CREATE TABLE group_test (
    group_id TEXT NO NULL,
    test_id TEXT NOT NULL,
    created_at TEXT NOT NULL DEFAULT current_timestamp,
    delete_state_code INTEGER NOT NULL DEFAULT 0,
    FOREIGN KEY (group_id) REFERENCES groups (group_id),
    FOREIGN KEY (test_id) REFERENCES tests (test_id)
)
