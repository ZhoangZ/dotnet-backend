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

 Date: 02/05/2021 22:32:41
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
  `params_url_status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `url_return` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `user_id`(`user_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of payment
-- ----------------------------
INSERT INTO `payment` VALUES (1, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=www.localhost.com&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=1&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=93ae49bb29f3cd834c2781e4d2c09122e7d0b9cd32e5cd524a00497fd747979a', 'http://sandbox.vnpayment.vn/merchant_webapi/merchant.html?vnp_Command=querydr&vnp_CreateDate=00010101000000&vnp_IpAddr=119.17.249.22&vnp_OrderInfo=dotnet Truy van luc 00010101000000&vnp_TmnCode=RSS5QRAC&vnp_TransDate=00010101000000&vnp_TxnRef=1&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=64346c5311b46c2a9e3ca498c02b6b1e2f35ef0e4e9ad022e3e9c8bd14d42061', NULL, NULL);
INSERT INTO `payment` VALUES (2, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=www.localhost.com&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=1&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=93ae49bb29f3cd834c2781e4d2c09122e7d0b9cd32e5cd524a00497fd747979a', 'http://sandbox.vnpayment.vn/merchant_webapi/merchant.html?vnp_Command=querydr&vnp_CreateDate=00010101000000&vnp_IpAddr=119.17.249.22&vnp_OrderInfo=dotnet Truy van luc 00010101000000&vnp_TmnCode=RSS5QRAC&vnp_TransDate=00010101000000&vnp_TxnRef=2&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=b3a1c8a1ca9533432e39700ffc533116f2aa20c1372c6df4b4bc07bcca2cd1a2', NULL, 'https://localhost:25002');
INSERT INTO `payment` VALUES (3, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=www.localhost.com&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=1&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=93ae49bb29f3cd834c2781e4d2c09122e7d0b9cd32e5cd524a00497fd747979a', 'http://sandbox.vnpayment.vn/merchant_webapi/merchant.html?vnp_Command=querydr&vnp_CreateDate=00010101000000&vnp_IpAddr=119.17.249.22&vnp_OrderInfo=dotnet Truy van luc 00010101000000&vnp_TmnCode=RSS5QRAC&vnp_TransDate=00010101000000&vnp_TxnRef=3&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=d4dc025c80176debbcc06d1f740c7e15f6d411e5c745b4ae2ecf1439fb6cdeb2', 'vnp_Message=Request_is_duplicate&vnp_ResponseCode=94', 'https://localhost:25002');
INSERT INTO `payment` VALUES (4, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=www.localhost.com&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=4&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=fa057711474d17086618d8be7d7f5fae52fa20612f3f64402c13c60131bf2bfd', NULL, NULL, 'https://localhost:25002');
INSERT INTO `payment` VALUES (5, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=https://localhost:25002/payment/donate/5&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=5&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=49374716e2d6b948b3c50498dae3dc2baecc4ba0f8e59447f7c0d8a25a59bd1c', 'http://sandbox.vnpayment.vn/merchant_webapi/merchant.html?vnp_Command=querydr&vnp_CreateDate=00010101000000&vnp_IpAddr=119.17.249.22&vnp_OrderInfo=dotnet Truy van luc 00010101000000&vnp_TmnCode=RSS5QRAC&vnp_TransDate=00010101000000&vnp_TxnRef=5&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=46e669f95b656c5d03da395a06a6cfb43246d715a1a06ae663414265e4a1b85d', NULL, 'https://localhost:25002');
INSERT INTO `payment` VALUES (6, NULL, '0001-01-01 00:00:00', 1000000, '119.17.249.22', 'VND', NULL, 'http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=1000000&vnp_Command=pay&vnp_CreateDate=00010101000000&vnp_CurrCode=VND&vnp_IpAddr=119.17.249.22&vnp_Locale=vn&vnp_OrderInfo=dotnet&vnp_ReturnUrl=https://localhost:25002/payment/donate/6&vnp_TmnCode=RSS5QRAC&vnp_TxnRef=6&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=2c93297b168637a684b9cb39ec5b57452d2ffaa38c61ed68498cf7c837a58bbf', 'http://sandbox.vnpayment.vn/merchant_webapi/merchant.html?vnp_Command=querydr&vnp_CreateDate=00010101000000&vnp_IpAddr=119.17.249.22&vnp_OrderInfo=dotnet Truy van luc 00010101000000&vnp_TmnCode=RSS5QRAC&vnp_TransDate=00010101000000&vnp_TxnRef=6&vnp_Version=2.0.0&vnp_SecureHashType=SHA256&vnp_SecureHash=2c8e501323e6528c488425c8b5e4f784fc26aa5048bca51582d93f8deccb787c', 'vnp_Amount=1000000&vnp_BankCode=NCB&vnp_Message=QueryDR+Success&vnp_OrderInfo=dotnet&vnp_PayDate=00010101070000&vnp_ResponseCode=00&vnp_TmnCode=RSS5QRAC&vnp_TransactionNo=13497837&vnp_TransactionStatus=00&vnp_TransactionType=01&vnp_TxnRef=6&vnp_SecureHash=b573c378c55448fcb0646733697c0f341248196f85b2430fb4146960b34411c0', 'https://localhost:25002');

SET FOREIGN_KEY_CHECKS = 1;
