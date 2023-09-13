import 'package:json_annotation/json_annotation.dart';

part 'categories.g.dart';

@JsonSerializable()
class Categories {
  late int id;
  late String categoryName;
  late DateTime? createdAt;
  late DateTime? deactivatedAt;

  Categories(
      {this.id = 0,
    this.categoryName = '',
    this.createdAt,
    this.deactivatedAt
  }
  );

  factory Categories.fromJson(Map<String, dynamic> json) => _$CategoriesFromJson(json);

  Map<String, dynamic> toJson() => _$CategoriesToJson(this);
}