PGDMP     .                    {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            	           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            
           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    Cards    TABLE     ]   CREATE TABLE public."Cards" (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public."Cards";
       public         heap    postgres    false            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    loyverse_customer_id character varying(150) NOT NULL,
    date_of_birth bytea NOT NULL,
    email bytea NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false            �            1259    24601 	   employees    TABLE     �   CREATE TABLE public.employees (
    user_name character varying(30) NOT NULL,
    password character varying(20) NOT NULL,
    is_admin boolean DEFAULT false NOT NULL
);
    DROP TABLE public.employees;
       public         heap    postgres    false                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214   J                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   �                 0    24601 	   employees 
   TABLE DATA           B   COPY public.employees (user_name, password, is_admin) FROM stdin;
    public          postgres    false    216   �       n           2606    16406    Cards Cards_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (card_id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    214            p           2606    24670    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    215            r           2606    24676    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            t           2606    24605    employees employees_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (user_name);
 B   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_pkey;
       public            postgres    false    216               �   x�E�;1�zs�؞��HK�Hr�����i����Y<��\�flbS6u�6�����a��=|ᴅ@���5�T4=�u萡��VN�7�O"��K��tp�aND��Æ
s��y��g&��Q����N3<         �   x�%��q�@C�q/�$����w]C���ɉIO���[kOb�|���Z����9F<y�\�Lš`!2��b*HK�ab�D)��HX*����l�e�ft�ۢl���zHd�\��M����-M������ɳ�`�F��E��G�#��������G�M�VՏxX'���ݶ��Bk         0   x���L*J-H�+NL�/JL����t�KL�I�3�K�@�%\1z\\\ ��D     