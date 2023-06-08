PGDMP                         {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    Cards    TABLE     ]   CREATE TABLE public."Cards" (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public."Cards";
       public         heap    postgres    false                       0    0    TABLE "Cards"    ACL     ;   GRANT SELECT,UPDATE ON TABLE public."Cards" TO client_api;
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
       public         heap    postgres    false            �            1259    25897    log_entries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.log_entries_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.log_entries_id_seq;
       public          postgres    false    218                       0    0    log_entries_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.log_entries_id_seq OWNED BY public.log_entries.id;
          public          postgres    false    217            �            1259    24601    users    TABLE     �   CREATE TABLE public.users (
    is_admin boolean DEFAULT false NOT NULL,
    password bytea NOT NULL,
    user_name character varying(60) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false                       0    0    TABLE users    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO client_api;
          public          postgres    false    216            r           2604    25901    log_entries id    DEFAULT     p   ALTER TABLE ONLY public.log_entries ALTER COLUMN id SET DEFAULT nextval('public.log_entries_id_seq'::regclass);
 =   ALTER TABLE public.log_entries ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    217    218    218                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214   '                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   �                 0    25898    log_entries 
   TABLE DATA           b   COPY public.log_entries (id, log_level, message, exception, stack_trace, occurred_on) FROM stdin;
    public          postgres    false    218   .                 0    24601    users 
   TABLE DATA           >   COPY public.users (is_admin, password, user_name) FROM stdin;
    public          postgres    false    216   K                  0    0    log_entries_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.log_entries_id_seq', 1, false);
          public          postgres    false    217            u           2606    16406    Cards Cards_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (card_id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
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
       public            postgres    false    216            ~           2606    25892    Cards Cards_customer_email_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_customer_email_fkey" FOREIGN KEY (customer_email) REFERENCES public.customers(email) NOT VALID;
 M   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_customer_email_fkey";
       public          postgres    false    3191    215    214               �   x�E�A��0��c�DQ�'hϹ�q��>�E��E�|���oy�c.���X���8> ���ܣF�=�����M?�[8�O�6a�09�J���'Δ����CǛ���1�z�M:�r8�+UYL��V�[�G��)yKD*��
�q�J�S��#������Xj\��NL)v�ASp��(�O)��.K�         -  x�5�Qr� C����,�p��	���W춓��Ė��x�{>i��b�ԇ!�ƞ��%�^��0v]܃��9͈�Ѽ����NX��i�NW�q�K��2Mw��{v���	�[q\@�֭��#��NJ�)�V���h(T,L���/q�'gc=����Iu�0̠�`{P�F�i+�\�}��H;�'����Eڣ����d�� �,,��d4���[K+�?�FF�"]��h���\��u����Gq\E��#�pe�B�u�����;ǿKP�MKe���..�,��`���������|�            x������ � �         y   x��;�0D��0(���:w�n�CJA@���i����K)�$H�N��tjӇNnec4Q�S�U�J�Jj��6)�AF\�j6/^�x�Y�������v��<>u��:���-����%     