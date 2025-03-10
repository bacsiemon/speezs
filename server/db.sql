create table users
(
    user_id              serial
        primary key,
    email                varchar(128) not null
        unique,
    password_hash        varchar(256) not null,
    password_salt        varchar(64)  not null,
    phone_number         varchar(10),
    full_name            varchar(100),
    profile_image_url    varchar(255),
    last_login           timestamp,
    is_deleted           boolean   default false,
    date_created         timestamp default CURRENT_TIMESTAMP,
    date_modified        timestamp,
    date_deleted         timestamp,
    refresh_token        varchar(64),
    refresh_token_expiry timestamp
);

alter table users
    owner to postgres;

create table userpreferences
(
    preference_id        serial
        primary key,
    user_id              integer
        unique
        references users,
    skin_tone            varchar(50),
    skin_type            varchar(50),
    face_shape           varchar(50),
    eye_color            varchar(50),
    preferred_brands     text,
    allergies            text,
    ai_enhancement_level integer   default 5,
    is_deleted           boolean   default false,
    date_created         timestamp default CURRENT_TIMESTAMP,
    date_modified        timestamp,
    date_deleted         timestamp
);

alter table userpreferences
    owner to postgres;

create table subscriptiontiers
(
    tier_id               serial
        primary key,
    name                  varchar(50)    not null,
    description           text,
    price                 numeric(10, 2) not null,
    duration_days         integer        not null,
    max_transfers         integer,
    max_collections       integer,
    allows_commercial_use boolean   default false,
    priority_processing   boolean   default false,
    is_active             boolean   default true,
    is_deleted            boolean   default false,
    date_created          timestamp default CURRENT_TIMESTAMP,
    date_modified         timestamp,
    date_deleted          timestamp
);

alter table subscriptiontiers
    owner to postgres;

create table usersubscriptions
(
    user_subscription_id serial
        primary key,
    user_id              integer
        references users,
    tier_id              integer
        references subscriptiontiers,
    start_date           timestamp   not null,
    end_date             timestamp   not null,
    status               varchar(20) not null,
    payment_method       varchar(50),
    auto_renew           boolean   default true,
    last_billing_date    timestamp,
    next_billing_date    timestamp,
    cancellation_date    timestamp,
    is_deleted           boolean   default false,
    date_created         timestamp default CURRENT_TIMESTAMP,
    date_modified        timestamp,
    date_deleted         timestamp
);

alter table usersubscriptions
    owner to postgres;

create table looks
(
    look_id         serial
        primary key,
    name            varchar(100) not null,
    description     text,
    created_by      integer
        references users,
    is_public       boolean   default true,
    category        varchar(50),
    thumbnail_url   varchar(255),
    avg_rating      numeric(3, 2),
    total_transfers integer   default 0,
    is_deleted      boolean   default false,
    date_created    timestamp default CURRENT_TIMESTAMP,
    date_modified   timestamp,
    date_deleted    timestamp
);

alter table looks
    owner to postgres;

create table makeupproducts
(
    product_id    serial
        primary key,
    name          varchar(255) not null,
    brand         varchar(100),
    category      varchar(50),
    color_code    varchar(7),
    description   text,
    image_url     varchar(255),
    is_verified   boolean   default false,
    is_deleted    boolean   default false,
    date_created  timestamp default CURRENT_TIMESTAMP,
    date_modified timestamp,
    date_deleted  timestamp
);

alter table makeupproducts
    owner to postgres;

create table lookproducts
(
    look_product_id   serial
        primary key,
    look_id           integer
        references looks,
    product_id        integer
        references makeupproducts,
    application_area  varchar(50),
    intensity         numeric(3, 2),
    application_order integer,
    notes             text,
    is_deleted        boolean   default false,
    date_created      timestamp default CURRENT_TIMESTAMP,
    date_modified     timestamp,
    date_deleted      timestamp
);

alter table lookproducts
    owner to postgres;

create table favoritecollections
(
    collection_id serial
        primary key,
    user_id       integer
        references users,
    name          varchar(100),
    description   text,
    is_private    boolean   default false,
    is_deleted    boolean   default false,
    date_created  timestamp default CURRENT_TIMESTAMP,
    date_modified timestamp,
    date_deleted  timestamp
);

alter table favoritecollections
    owner to postgres;

create table collectionlooks
(
    collection_look_id serial
        primary key,
    collection_id      integer
        references favoritecollections,
    look_id            integer
        references looks,
    notes              text,
    is_deleted         boolean   default false,
    date_created       timestamp default CURRENT_TIMESTAMP,
    date_modified      timestamp,
    date_deleted       timestamp
);

alter table collectionlooks
    owner to postgres;

create table reviews
(
    review_id     serial
        primary key,
    look_id       integer
        references looks,
    user_id       integer
        references users,
    rating        integer
        constraint reviews_rating_check
            check ((rating >= 1) AND (rating <= 5)),
    comment       text,
    helpful_votes integer   default 0,
    is_deleted    boolean   default false,
    date_created  timestamp default CURRENT_TIMESTAMP,
    date_modified timestamp,
    date_deleted  timestamp
);

alter table reviews
    owner to postgres;

create table transfers
(
    transfer_id      serial
        primary key,
    user_id          integer
        references users,
    look_id          integer
        references looks,
    source_image_url varchar(255),
    result_image_url varchar(255),
    processing_time  numeric(5, 2),
    status           varchar(20),
    ai_model_version varchar(50),
    is_deleted       boolean   default false,
    date_created     timestamp default CURRENT_TIMESTAMP,
    date_modified    timestamp,
    date_deleted     timestamp
);

alter table transfers
    owner to postgres;

create table userresetpasswordcodes
(
    email     varchar(128) not null
        primary key,
    code_hash varchar(256) not null,
    expire    timestamp    not null
);

alter table userresetpasswordcodes
    owner to postgres;

create table userclaims
(
    user_id     integer                                          not null
        constraint user_id
            references users,
    claim_id    integer                                          not null
        constraint userclaims_pk
            primary key,
    claim_type  varchar(64) default 'UNKNOWN'::character varying not null,
    claim_value varchar(64) default 'UNKNOWN'::character varying not null
);

alter table userclaims
    owner to postgres;

