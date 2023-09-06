import 'package:json_annotation/json_annotation.dart';

part 'task.g.dart';

@JsonSerializable()
class Task {
  late int id;
  late String taskName;
  late DateTime? createdAt;
  late DateTime? completedAt;
  late DateTime? deactivatedAt;
  late DateTime? deletedAt;

  Task(
      {this.id = 0,
      this.taskName = '',
      this.createdAt,
      this.completedAt,
      this.deactivatedAt,
      this.deletedAt});

  factory Task.fromJson(Map<String, dynamic> json) => _$TaskFromJson(json);

  Map<String, dynamic> toJson() => _$TaskToJson(this);
}
