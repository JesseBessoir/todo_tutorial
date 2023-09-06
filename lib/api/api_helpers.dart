import 'dart:convert';
import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:http/http.dart' as http;

import '../models/task.dart';

String baseUrl =
    'https://ba47-2603-6080-c301-9538-1d29-7874-2453-b945.ngrok-free.app/api/';

Future<http.Response> post(String path, dynamic payload,
    {bool timeOut = false}) async {
  try {
    if (timeOut) {}
    http.Response response = await http
        .post(
          Uri.parse(baseUrl + path),
          headers: <String, String>{
            'Accept': 'application/json',
            'Content-Type': 'application/json; charset=UTF-8',
          },
          body: jsonEncode(payload),
        )
        .timeout(const Duration(seconds: 120),
            onTimeout: () => http.Response("Error", 408));
    return response;
  } on Exception catch (ex, stack) {
    rethrow;
  }
}

Future<dynamic> fetch(String path, dynamic parameters) async {
  try {
    var params = Uri(queryParameters: parameters).query;
    var fullpath = Uri.parse('$baseUrl$path/?$params');
    return await http.get(
      fullpath,
      headers: <String, String>{
        'Accept': 'application/json',
        'Content-Type': 'application/json; charset=UTF-8',
      },
    );
  } on Exception catch (ex, stack) {
    rethrow;
  }
}

Future<List<Task>> getTaskList() async {
  dynamic response = await fetch('Public/GetTaskList', null);
  if (response.statusCode == 200) {
    return (jsonDecode(response.body) as List)
        .map((e) => (Task.fromJson(e)))
        .toList();
  }
  return <Task>[];
}
