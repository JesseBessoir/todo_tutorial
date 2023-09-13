import 'package:json_annotation/json_annotation.dart';

import 'categories.dart';

part 'task.g.dart';

@JsonSerializable()
class Task {
  late int id;
  late String taskName;
  late DateTime? createdAt;
  late DateTime? completedAt;
  late DateTime? deactivatedAt;
  late DateTime? deletedAt;
  late List<Categories> categories;

  Task(
      {this.id = 0,
      this.taskName = '',
      this.createdAt,
      this.completedAt,
      this.deactivatedAt,
      this.deletedAt,
        categories
      })
      : categories = categories ?? [];


  factory Task.fromJson(Map<String, dynamic> json) => _$TaskFromJson(json);

  Map<String, dynamic> toJson() => _$TaskToJson(this);
}
