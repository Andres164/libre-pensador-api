PGDMP         ;                {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    cards    TABLE     [   CREATE TABLE public.cards (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public.cards;
       public         heap    postgres    false                       0    0    TABLE cards    ACL     9   GRANT SELECT,UPDATE ON TABLE public.cards TO client_api;
          public          postgres    false    214            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    loyverse_customer_id character varying(150) NOT NULL,
    date_of_birth bytea NOT NULL,
    email bytea NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false                       0    0    TABLE customers    ACL     D   GRANT SELECT,INSERT,DELETE ON TABLE public.customers TO client_api;
          public          postgres    false    215            �            1259    25898    log_entries    TABLE     �   CREATE TABLE public.log_entries (
    id integer NOT NULL,
    log_level character varying(50) NOT NULL,
    message text NOT NULL,
    exception text,
    stack_trace text,
    occurred_on timestamp without time zone DEFAULT now() NOT NULL
);
    DROP TABLE public.log_entries;
       public         heap    postgres    false                       0    0    TABLE log_entries    ACL     ?   GRANT SELECT,INSERT ON TABLE public.log_entries TO client_api;
          public          postgres    false    218            �            1259    25897    log_entries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.log_entries_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.log_entries_id_seq;
       public          postgres    false    218                       0    0    log_entries_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.log_entries_id_seq OWNED BY public.log_entries.id;
          public          postgres    false    217                       0    0    SEQUENCE log_entries_id_seq    ACL     H   GRANT SELECT,USAGE ON SEQUENCE public.log_entries_id_seq TO client_api;
          public          postgres    false    217            �            1259    24601    users    TABLE     �   CREATE TABLE public.users (
    is_admin boolean DEFAULT false NOT NULL,
    password bytea NOT NULL,
    user_name character varying(60) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false                       0    0    TABLE users    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO client_api;
          public          postgres    false    216            r           2604    25901    log_entries id    DEFAULT     p   ALTER TABLE ONLY public.log_entries ALTER COLUMN id SET DEFAULT nextval('public.log_entries_id_seq'::regclass);
 =   ALTER TABLE public.log_entries ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    218    218                      0    16399    cards 
   TABLE DATA           8   COPY public.cards (card_id, customer_email) FROM stdin;
    public          postgres    false    214   �                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   �                 0    25898    log_entries 
   TABLE DATA           b   COPY public.log_entries (id, log_level, message, exception, stack_trace, occurred_on) FROM stdin;
    public          postgres    false    218                    0    24601    users 
   TABLE DATA           >   COPY public.users (is_admin, password, user_name) FROM stdin;
    public          postgres    false    216   ,                  0    0    log_entries_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.log_entries_id_seq', 1, false);
          public          postgres    false    217            u           2606    16406    cards cards_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.cards
    ADD CONSTRAINT cards_pkey PRIMARY KEY (card_id);
 :   ALTER TABLE ONLY public.cards DROP CONSTRAINT cards_pkey;
       public            postgres    false    214            w           2606    24670    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    215            y           2606    24676    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            }           2606    25906    log_entries log_entries_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.log_entries
    ADD CONSTRAINT log_entries_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.log_entries DROP CONSTRAINT log_entries_pkey;
       public            postgres    false    218            {           2606    25891    users users_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_name);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216            ~           2606    25892    cards Cards_customer_email_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.cards
    ADD CONSTRAINT "Cards_customer_email_fkey" FOREIGN KEY (customer_email) REFERENCES public.customers(email) NOT VALID;
 K   ALTER TABLE ONLY public.cards DROP CONSTRAINT "Cards_customer_email_fkey";
       public          postgres    false    3191    215    214               ,   x�2 3�?�@0���@0�LS��4A0A�=... �M            x������ � �            x������ � �         �   x�e��r�0Cg�cz���?t��E��M{I�~~�8�^˾�XQ���쮩�S��gl,f*,��`�ft�m%up�ᛌD	!�]�ݨ��x�#<Yq�ŮZY>o�1����ϣ���=����J�ޤ�N4�e�yC�/,N��)��@=0ɨ@W���_�@V��ʣ �З��|��#����D(     