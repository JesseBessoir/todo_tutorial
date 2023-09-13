// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'task_search_criteria.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

TaskSearchCriteria _$TaskSearchCriteriaFromJson(Map<String, dynamic> json) =>
    TaskSearchCriteria(
      completedAt: json['completedAt'] as bool? ?? false,
      categoryIds: (json['categoryIds'] as List<dynamic>?)
          ?.map((e) => e as int)
          .toList(),
    );

Map<String, dynamic> _$TaskSearchCriteriaToJson(TaskSearchCriteria instance) =>
    <String, dynamic>{
      'completedAt': instance.completedAt,
      'categoryIds': instance.categoryIds,
    };
