// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'priority.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Priority _$PriorityFromJson(Map<String, dynamic> json) => Priority(
      id: json['id'] as int? ?? 0,
      Name: json['name'] as String? ?? '',
      Sequence: json['sequence'] as int? ?? 0,
    );

Map<String, dynamic> _$PriorityToJson(Priority instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.Name,
      'sequence': instance.Sequence,
    };
