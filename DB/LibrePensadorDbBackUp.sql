PGDMP     *    4                {            CafeLibrePensador    15.2    15.2     	           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            
           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    Cards    TABLE     n   CREATE TABLE public."Cards" (
    card_id character(7) NOT NULL,
    customer_email character varying(100)
);
    DROP TABLE public."Cards";
       public         heap    postgres    false            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    email character varying(100) NOT NULL,
    date_of_birth date NOT NULL,
    loyverse_customer_id character varying(150) NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false            �            1259    24601 	   employees    TABLE     �   CREATE TABLE public.employees (
    user_name character varying(30) NOT NULL,
    password character varying(20) NOT NULL,
    is_admin boolean DEFAULT false NOT NULL
);
    DROP TABLE public.employees;
       public         heap    postgres    false                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214                    0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (email, date_of_birth, loyverse_customer_id) FROM stdin;
    public          postgres    false    215   �                 0    24601 	   employees 
   TABLE DATA           B   COPY public.employees (user_name, password, is_admin) FROM stdin;
    public          postgres    false    216   d       n           2606    16406    Cards Cards_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (card_id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    214            p           2606    16408    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            t           2606    24605    employees employees_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (user_name);
 B   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_pkey;
       public            postgres    false    216            r           2606    16412    customers loyverse_customer_id 
   CONSTRAINT     i   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT loyverse_customer_id UNIQUE (loyverse_customer_id);
 H   ALTER TABLE ONLY public.customers DROP CONSTRAINT loyverse_customer_id;
       public            postgres    false    215            u           2606    24606    Cards Cards_customer_email_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_customer_email_fkey" FOREIGN KEY (customer_email) REFERENCES public.customers(email) NOT VALID;
 M   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_customer_email_fkey";
       public          postgres    false    215    214    3184               b   x�2 3�?�@0���@0MLC�ļ���bc�$��������\��	�knb^ij���1grFf^~qAQ~Veqi1�N#N�̜�ļD��=... �c-�         �   x�U�;r�0k�.�H ��t9B~�DY�X�"�����k���/�R�z��e���6�0B,a$3v���
����Z�n��6�~0�v��I�'G����l�Ԁ{���.h8Z�]4�������"?��Q�p#�y�7͠�7(L޴����l��s߿���j���3�$%��,���2�,>ucI��B�͝J�         0   x���L*J-H�+NL�/JL����t�KL�I�3�K�@�%\1z\\\ ��D     