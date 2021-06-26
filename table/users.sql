/*
 Navicat Premium Data Transfer

 Source Server         : demo
 Source Server Type    : MySQL
 Source Server Version : 100418
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100418
 File Encoding         : 65001

 Date: 25/06/2021 18:21:41
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `fullname` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `provider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `blocked` bit(1) NULL DEFAULT b'0',
  `active` bit(1) NULL DEFAULT b'1',
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `opt` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 29 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;


-- ----------------------------
-- Triggers structure for table users
-- ----------------------------
DROP TRIGGER IF EXISTS `after_insert_users`;
delimiter ;;
CREATE TRIGGER `after_insert_users` AFTER INSERT ON `users` FOR EACH ROW BEGIN
				INSERT INTO cart ( user_id, total_item,total_price) VALUES (new.id, 0,0);
				

	 END
;;
delimiter ;

-- ----------------------------
-- Triggers structure for table users
-- ----------------------------
DROP TRIGGER IF EXISTS `before_delete_users`;
delimiter ;;
CREATE TRIGGER `before_delete_users` BEFORE DELETE ON `users` FOR EACH ROW BEGIN
				DELETE FROM user_role WHERE users_id=old.id;
				DELETE FROM cart WHERE user_id=old.id;
				

	 END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (25, 'tanhoang99.999@gmail.com', 'Tấn Hoàng', '0399115950', 'Bình Dương', NULL, b'0', b'1', '0738D0A6141D4E17DB5FCADB94EE0D7A', NULL, NULL);
INSERT INTO `users` VALUES (26, 'thiendaopk1@gmail.com', 'Đào  Chí Thiện', '0399115950', 'Thủ Đúc', NULL, b'0', b'1', '9B96DC2A8B1BA2CCF23EDAA930FC83BD', NULL, NULL);
INSERT INTO `users` VALUES (27, 'ongdinh6@gmail.com', 'Ông Minh Đình', '0988766567', '47/16 Đường số 10, khu phố 3, phường Linh Xuân, Thành phố Thủ Đức, HCM', NULL, b'0', b'1', '9325179D3AC9D30B9093C86ACD2F6237', NULL, NULL);
INSERT INTO `users` VALUES (28, 'admin@gmail.com', 'Nguyen Van A', '0988766567', 'Đường số 10, phường Linh Trung, Thành phố Thủ Đức, HCM', NULL, b'0', b'1', '9325179D3AC9D30B9093C86ACD2F6237', NULL, NULL);

