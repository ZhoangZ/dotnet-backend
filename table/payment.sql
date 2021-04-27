/*
 Navicat Premium Data Transfer

 Source Server         : My SQL
 Source Server Type    : MySQL
 Source Server Version : 100417
 Source Host           : localhost:3306
 Source Schema         : dotnet

 Target Server Type    : MySQL
 Target Server Version : 100417
 File Encoding         : 65001

 Date: 27/04/2021 17:59:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for payment
-- ----------------------------
DROP TABLE IF EXISTS `payment`;
CREATE TABLE `payment`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user_id` bigint(20) NULL DEFAULT NULL,
  `create_time` datetime(0) NULL DEFAULT utc_timestamp,
  `amount` decimal(65, 0) NULL DEFAULT NULL,
  `ip_address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `currcode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `transaction_Status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `url_pay` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `url_status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of payment
-- ----------------------------
INSERT INTO `payment` VALUES (15, 1, '2021-04-25 17:36:20', 10000, '119.17.249.22', 'VND', 'UNCOMPLETE', NULL, NULL);
INSERT INTO `payment` VALUES (16, 1, '2021-04-25 17:49:53', 10000, '119.17.249.22', 'VND', 'UNCOMPLETE', NULL, NULL);
INSERT INTO `payment` VALUES (17, 1, '2021-04-25 18:34:14', 10000, '119.17.249.22', 'VND', 'UNCOMPLETE', NULL, NULL);
INSERT INTO `payment` VALUES (18, 1, '2021-04-25 18:36:48', 10000, '119.17.249.22', 'VND', 'UNCOMPLETE', NULL, NULL);
INSERT INTO `payment` VALUES (19, 1, '2021-04-25 18:37:30', 10000, '119.17.249.22', 'VND', 'COMPLETE', 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=20210426013730&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=Day+la+mieu+ta&vnp_ReturnUrl=http%3A%2F%2Flocalhost%3A25001&vnp_TmnCode=67LF6OWG&vnp_TxnRef=19&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=6ddfbc525227fcbdae4f938ed4716d9684af15a53e9e65088706f3f616455368', NULL);
INSERT INTO `payment` VALUES (21, NULL, '0001-01-01 00:00:00', 0, NULL, NULL, NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=www.localhost.com&vnp_TmnCode=67LF6OWG&vnp_TxnRef=1&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=99792b53e11fa3487e95026bb7053113dec056986380ecd2df0aefaeb8805a4d', NULL);

SET FOREIGN_KEY_CHECKS = 1;
