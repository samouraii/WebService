-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Client :  127.0.0.1
-- Généré le :  Jeu 26 Janvier 2017 à 02:52
-- Version du serveur :  5.7.14
-- Version de PHP :  7.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `techinfo`
--
CREATE DATABASE IF NOT EXISTS `techinfo` DEFAULT CHARACTER SET utf8 COLLATE utf8_bin;
USE `techinfo`;

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `numdossier` int(11) NOT NULL,
  `nom` varchar(50) CHARACTER SET latin1 NOT NULL,
  `numtva` varchar(20) CHARACTER SET latin1 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`id`, `numdossier`, `nom`, `numtva`) VALUES
(28, 88, 'TextBox', 'TextBox'),
(29, 87, 'tutu', 'TextBox'),
(30, 77, 'tututu', 'TextBox');

-- --------------------------------------------------------

--
-- Structure de la table `clienttouser`
--

CREATE TABLE `clienttouser` (
  `idclient` int(11) NOT NULL,
  `iduser` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `clienttouser`
--

INSERT INTO `clienttouser` (`idclient`, `iduser`) VALUES
(30, 6);

-- --------------------------------------------------------

--
-- Structure de la table `transaction`
--

CREATE TABLE `transaction` (
  `id` int(11) NOT NULL,
  `achat` int(11) DEFAULT NULL,
  `tva` int(11) NOT NULL,
  `prixhtv` double NOT NULL,
  `voiture` text COLLATE utf8_bin NOT NULL,
  `client` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `transaction`
--

INSERT INTO `transaction` (`id`, `achat`, `tva`, `prixhtv`, `voiture`, `client`) VALUES
(1, NULL, 88, 980, 'kjb', 30);

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `username` varchar(50) CHARACTER SET latin1 NOT NULL,
  `salt` varchar(50) CHARACTER SET latin1 NOT NULL,
  `mdp` varchar(50) CHARACTER SET latin1 NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id`, `username`, `salt`, `mdp`) VALUES
(6, 'test', 'fd8e1b2ec2f403c60d5085b62722b704', 'test'),
(7, 'test2 ', '893fdd0d97b20d4da13e1ef343636d5b', 'toto'),
(8, 'titi', '60b72a20f9bc759b6d468fc0b69ead3d', 'titi'),
(10, 'TextBox2', 'a9ab5c957e71f866fc7b578f92c625fe', '1'),
(11, 'TextBoxn', '5155f1f182f66edeaa2202d14cd03523', ','),
(12, 'tyty', '1c58d73d2a6984543b400440c84c0fe4', 'aaaa'),
(13, 'bhk', '0007035b28b2bcbc1175a7fec8197729', 'ttt'),
(14, 'TextBoxqsd', 'f8571359915879917df4f676d5f8f768', 'tutu'),
(15, 'marine', '2bf5197cfd558254fb9cc840b513f556', 'toto'),
(16, 'marine2', 'd373c2255e2b0d540e57a392598c9f82', 'toto'),
(17, 'Marine3', '5fc350ca33ca40bf42b714697c2e3c2d', 'toto'),
(18, 'yoyo', 'ce423bbc2c6de1555a240efd393b83f6', 'toto'),
(19, 'titititi', 'a9152f2370c86686302ad78225d71fd6', 'toto'),
(20, 'tytyt', 'b87346c41c608aa3295a7b3de838f71e', 'toto'),
(21, 'TextBox', '40d5b81b16987ab4c5866b1ebead7d04', 'titi'),
(22, 'tytyty', 'a111ad17a6ca242ff39f11baadadf1cf', 'tyty'),
(23, 'TextBoxzak', '38597cebbfe37b3791704ea80e6ad33f', 'tutu'),
(24, 'Marine4', '10634a8395ea778beb99974bb31f6ee1', 'toto'),
(26, 'Marine5', '8bbc7f131a521441db157ffdcd857088', 'toto'),
(27, 'tutu', '67c06ff1a94c0b6de5222f20e75bc00e', 'tutu'),
(28, 'tututu', '41ad212fdaec614d38a1e90c2eac65f2', 'tutu'),
(29, 'TextBox444', 'fef3b474b0cf4d44036daa8e4f8afd04', 'toto'),
(30, 'adrien', '298505d3adc383ffbf744e2114c3a20a', '58255c2239ff14d7ba9e0f3c8168ab01');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `numdossier_2` (`id`),
  ADD UNIQUE KEY `numdossier_3` (`numdossier`),
  ADD KEY `numdossier` (`id`);

--
-- Index pour la table `clienttouser`
--
ALTER TABLE `clienttouser`
  ADD KEY `iduser` (`iduser`),
  ADD KEY `idclient` (`idclient`);

--
-- Index pour la table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`id`),
  ADD KEY `client` (`client`),
  ADD KEY `achat` (`achat`);

--
-- Index pour la table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `client`
--
ALTER TABLE `client`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
--
-- AUTO_INCREMENT pour la table `transaction`
--
ALTER TABLE `transaction`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT pour la table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;
--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `clienttouser`
--
ALTER TABLE `clienttouser`
  ADD CONSTRAINT `clienttouser_ibfk_1` FOREIGN KEY (`iduser`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `clienttouser_ibfk_2` FOREIGN KEY (`idclient`) REFERENCES `client` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`client`) REFERENCES `client` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `transaction_ibfk_2` FOREIGN KEY (`achat`) REFERENCES `transaction` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
