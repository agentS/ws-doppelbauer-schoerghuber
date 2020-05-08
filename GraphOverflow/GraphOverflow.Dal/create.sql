CREATE TABLE app_user(
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL
);

CREATE TABLE answer(
    id SERIAL PRIMARY KEY NOT NULL,
    title VARCHAR(255),
    content TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL,
    up_votes INT DEFAULT 0,
    question_id INT,
    user_id INT,
    CONSTRAINT fk_answer_question FOREIGN KEY (question_id) REFERENCES answer(id),
    CONSTRAINT fk_answer_user FOREIGN KEY (user_id) REFERENCES app_user(id)
);

CREATE TABLE answer_up_vote(
    user_id INT,
    answer_id INT,

    CONSTRAINT fk_answer_up_vote_answer FOREIGN KEY (answer_id) REFERENCES answer(id),
    CONSTRAINT fk_answer_up_vote_user FOREIGN KEY (user_id) REFERENCES app_user(id),
    CONSTRAINT pk_answer_up_vote PRIMARY KEY(user_id, answer_id)
);

CREATE TABLE comment(
    id SERIAL PRIMARY KEY NOT NULL,
    content TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL,
    answer_id INT,
    user_id INT,
    CONSTRAINT fk_comment_answer FOREIGN KEY (answer_id) REFERENCES answer(id),
    CONSTRAINT fk_comment_user FOREIGN KEY (user_id) REFERENCES app_user(id)
);

CREATE TABLE tag(
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(255)
);

CREATE TABLE tag_answer(
    tag_id INT,
    answer_id INT,

    CONSTRAINT fk_tag_answer_answer FOREIGN KEY (answer_id) REFERENCES answer(id),
    CONSTRAINT fk_tag_answer_tag FOREIGN KEY (tag_id) REFERENCES tag(id),
    CONSTRAINT pk_tag_answer PRIMARY KEY(tag_id, answer_id)
);

INSERT INTO tag(name) VALUES ('Java');
INSERT INTO tag(name) VALUES ('CSharp');
INSERT INTO tag(name) VALUES ('Go');
INSERT INTO app_user(name, password_hash) VALUES ('homer', 'homer');
INSERT INTO app_user(name, password_hash) VALUES ('bart', 'bart');
INSERT INTO answer(title, content, created_at, question_id, user_id)
VALUES ('question1', 'question1 content ...', now(), null, 1);
INSERT INTO answer(title, content, created_at, question_id, user_id)
VALUES ('answer1', 'answer1 content ...', now(), 1, 1);
INSERT INTO answer_up_vote(user_id, answer_id)
VALUES (1, 1);
INSERT INTO answer_up_vote(user_id, answer_id)
VALUES (1, 2);
INSERT INTO answer_up_vote(user_id, answer_id)
VALUES (2, 2);
INSERT INTO comment(content, created_at, answer_id, user_id)
VALUES ('comment', now(), 2, 2);
INSERT INTO tag_answer(tag_id, answer_id) VALUES (1,1);

commit;
