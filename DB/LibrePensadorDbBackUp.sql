PGDMP         #    	            {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    Cards    TABLE     ]   CREATE TABLE public."Cards" (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public."Cards";
       public         heap    postgres    false                       0    0    TABLE "Cards"    ACL     ;   GRANT SELECT,UPDATE ON TABLE public."Cards" TO client_api;
          public          postgres    false    214            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    loyverse_customer_id character varying(150) NOT NULL,
    date_of_birth bytea NOT NULL,
    email bytea NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false                       0    0    TABLE customers    ACL     D   GRANT SELECT,INSERT,DELETE ON TABLE public.customers TO client_api;
          public          postgres    false    215            �            1259    24601    users    TABLE     �   CREATE TABLE public.users (
    is_admin boolean DEFAULT false NOT NULL,
    user_name bytea NOT NULL,
    password bytea NOT NULL,
    user_number integer NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false                       0    0    TABLE users    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO client_api;
          public          postgres    false    216            �            1259    24687    users_user_number_seq    SEQUENCE     �   ALTER TABLE public.users ALTER COLUMN user_number ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.users_user_number_seq
    START WITH 0
    INCREMENT BY 1
    MINVALUE 0
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214                    0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   !                 0    24601    users 
   TABLE DATA           K   COPY public.users (is_admin, user_name, password, user_number) FROM stdin;
    public          postgres    false    216   �                  0    0    users_user_number_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.users_user_number_seq', 1, true);
          public          postgres    false    217            o           2606    16406    Cards Cards_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (card_id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    214            q           2606    24670    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    215            s           2606    24676    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            u           2606    24686    users users_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_name);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216            w           2606    24695    users users_user_number_key 
   CONSTRAINT     ]   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_user_number_key UNIQUE (user_number);
 E   ALTER TABLE ONLY public.users DROP CONSTRAINT users_user_number_key;
       public            postgres    false    216               �   x�EP[n�0�n3�&Eٗ�}�Ǎ�3��c�1��� >��)>z����y�~ø!n�������8�d�7���Uu���Ɋ���
fR()&�䚙�$W6O'�7�D��<��JtBŪ���������|+f��o�Q��`��n�ڑˎ/Wg�^WCֱ`�A)t��V�.���Q㩆�*O�����_�ə#�c7}Լ5jDQ�K/�[���Q�
x��kI��jά<�1�ͨ��!eG��r����k۶E�j�         �  x�ERI��@<���p�/uɍ7��'��������n��t;}���3��m�&�^��^�X@^f)��2ð}������-Ӗ-].!��iZ�]iy�����@Og_�t<(�w4�Ȕ��,�m�+�W ��N���2.�B�;{ȧ���͖!g75!���⩤ u xb�5�k�f�l1�bJ���t���ڧ�9��y(�#Xz��s��� "=�٥#�/�����@wV	6�3����t�����	/�� ��r�k
��E!�5&;n:S�ȍ�
�pe5�ݝAC�A9J6�^��Oy6g���Fz�o�xg4��ŋR�!�\Q�5�����&x����SP������0�a����&��̏�SW�Ě��K
�hL�������a�TL?�Sn�3&�f�W2��dVw꼒����MU���1���8�+Ǿ�         �   x�5�Qn� ����L$vz���PN�?���&�H��|�|]�Y��\$�72���%D&��?���33�`�8�O.y�����;���k�Ь׵�J�8$�W&��rD�O�ŧ6��]�Ɨ�����<�W���7�Vɢtg�7����y��#a�(����"��V4�4��������D�     