import 'package:flutter/scheduler.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:todo_tutorial/pages/current_list.dart';

part 'priority.g.dart';

@JsonSerializable()
class Priority {
  late int id;
  @JsonKey(name: 'name')
  late String Name;
  @JsonKey(name: 'sequence')
  late int Sequence;

  Priority(
      {
       this.id = 0,
       this.Name = '',
       this.Sequence = 0
      });

  factory Priority.fromJson(Map<String, dynamic> json) => _$PriorityFromJson(json);

  Map<String, dynamic> toJson() => _$PriorityToJson(this);
}
