import 'package:json_annotation/json_annotation.dart';

part 'task_search_criteria.g.dart';

@JsonSerializable()
class TaskSearchCriteria {
  late bool completedAt;
  late List<int>? categoryIds;

  TaskSearchCriteria(
      {
        this.completedAt = false,
        this.categoryIds,
      });

  factory TaskSearchCriteria.fromJson(Map<String, dynamic> json) => _$TaskSearchCriteriaFromJson(json);

  Map<String, dynamic> toJson() => _$TaskSearchCriteriaToJson(this);
}
