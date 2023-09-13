import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:multi_select_flutter/dialog/multi_select_dialog_field.dart';
import 'package:multi_select_flutter/util/multi_select_item.dart';
import 'package:multi_select_flutter/util/multi_select_list_type.dart';
import 'package:todo_tutorial/pages/completed_list.dart';

import '../models/categories.dart';
import '../models/task.dart';
import '../widgets/navbar.dart';
import '../widgets/todo_item.dart';
import 'package:todo_tutorial/api/api_helpers.dart';

class TodoList extends StatefulWidget {
  const TodoList({super.key, required this.title});

  final String title;

  @override
  State<TodoList> createState() => _TodoListState();
}

class _TodoListState extends State<TodoList> {
// In Flutter, a TextEditingController is a controller class that is commonly used to manage the text input in text form fields,
// such as TextField and TextFormField. It allows you to interact with and control the text entered by the user in these input fields.
// TextEditingController is part of the Flutter framework and provides methods and properties for reading, modifying, and listening to the text input.
  final TextEditingController _textFieldController = TextEditingController();
  bool refreshList = false;
  Type selectedValue = Categories;
  List<Categories> _categories = [];
  List<Categories> _selectedCategories = [];

  // we are including refresh list here so that we can trigger refreshlist whenever we do any of the actions like adding a new task or deleting one.


  Future toggleTaskCompleted(Task toggledTask) async {
    dynamic response = await post('Public/ToggleCompleted', toggledTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  Future saveTask(Task task) async {
    dynamic response = await post('Public/SaveTask', task);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  Future deactivateTask(Task deletedTask) async {
    dynamic response = await post('Public/DeactivateTask', deletedTask);
    if (response.statusCode == 200) {
      setState(() {
        refreshList = true;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    //getTaskList(completedBool: false, catIdList: null);
  }

  void _addTodoItem(String taskName) {
    var newTask = Task(taskName: taskName, createdAt: DateTime.now(), categories: _selectedCategories);
    saveTask(newTask).then((value) => {
          _textFieldController.clear(),
          setState(() {
            refreshList = true;
          })
        });
  }

  void _handleTodoChange(Task todo) {
    toggleTaskCompleted(todo).then((value) => {
          setState(() {
            refreshList = true;
          })
    });
  }

  void _deleteTodo(Task todo) {
    deactivateTask(todo).then((value) => {
          setState(() {
            refreshList = true;
          })
        });
  }

  @override
  Widget build(BuildContext context) {
    refreshList = true;

    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Column(
        children: <Widget>[
          Expanded(child:

          FutureBuilder(
            // FutureBuilders allows you to create widgets that depend on the completion of a Future and
            // automatically rebuilds your UI when the Future completes or changes its state.
              future: getTaskList(completedBool: false, catIdList: null),
              //declaring the future that will cause it to rebuild.
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return const CircularProgressIndicator();
                }
                if (snapshot.hasData) {

                  var todos = snapshot.data!;
                  return ListView.builder(
                    itemCount: todos?.length,
                    itemBuilder: (context, index) {
                      var todo = todos.elementAt(index);

                      return Dismissible(key: Key(todo.taskName),
                          //start of Dismissible
                          onDismissed: (DismissDirection direction) {
                            // Remove the item from the data source.
                            if (direction == DismissDirection.startToEnd){
                              setState(() {
                                _deleteTodo(todo);
                              });
                              ScaffoldMessenger.of(context)
                                  .showSnackBar(SnackBar(content: Text('${todo.taskName} dismissed')));
                          } else {
                              setState(() {
                                _handleTodoChange(todo);
                              });
                              ScaffoldMessenger.of(context)
                                  .showSnackBar(SnackBar(content: Text('${todo.taskName} COMPLETED!')));
                            }

                          }, //end of Dismissible logic
                          background: const ColoredBox(
                            color: Colors.red,
                            child: Align(
                              alignment: Alignment.centerLeft,
                              child: Padding(
                                padding: EdgeInsets.all(16.0),
                                child: Icon(Icons.delete, color: Colors.white),
                              ),
                            ),
                          ),
                          secondaryBackground: const ColoredBox(
                            color: Colors.green,
                            child: Align(
                              alignment: Alignment.centerRight,
                              child: Padding(
                                padding: EdgeInsets.all(16.0),
                                child: Icon(Icons.check_box, color: Colors.white),
                              ),
                            ),
                          ),
                          child: TaskItem(
                          todo: todo,
                          onTaskChanged: _handleTodoChange,
                          removeTask: _deleteTodo)
                      );
                    },
                  );
                }
                return Container(); //Handle errors or no items
              }),
          ),

          ElevatedButton(
            style: ElevatedButton.styleFrom(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
            ),
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute( builder: (context) => const SecondRoute()),
                ).then((value) => setState(() {}));
            },
            child: const Text('To Completed Page'),
          ),

          FloatingActionButton(
            onPressed: () => _displayDialog(),
            tooltip: 'Add a Todo',
            child: const Icon(Icons.add),
          ),

        ],
      )
    );
  }

  Future<void> _displayDialog() async {
    return showDialog<void>(
      context: context,
      barrierDismissible: false,
      builder: (BuildContext context) {
        return AlertDialog(
          //AlertDialog is how we will be showing the modal to add a new task
          title: const Text('Add a todo'),
          content: TextField(
            controller: _textFieldController,
            decoration:
                const InputDecoration(hintText: 'Add an item to your list'),
            autofocus: true,
          ),
          actions: <Widget>[

            // DropdownButton(
            // value: selectedValue,
            //     onChanged: (String? newValue){
            //       setState(() {
            //         selectedValue = newValue!;
            //       });
            //     },
            // items: dropdownItems
            // ),

            FutureBuilder(
                future: getCategoryList(completedBool: false, catIdList: null),
                builder: (context, snapshot) {
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    return const CircularProgressIndicator();
                  }
                  if (snapshot.hasData) {
                    print(snapshot.hasData);
                    _categories = snapshot.data!;


                    return MultiSelectDialogField(
                      items: _categories.map((e) => MultiSelectItem(e, e.categoryName)).toList(),
                      listType: MultiSelectListType.CHIP,
                      onConfirm: (values) {
                        _selectedCategories = values;
                      },
                    );



                  }
                  return Container(); //Handle errors or no items
                }),


            OutlinedButton(
              style: OutlinedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text('Cancel'),
            ),
            ElevatedButton(
              style: ElevatedButton.styleFrom(
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
              onPressed: () {
                _addTodoItem(_textFieldController.text);
                Navigator.of(context).pop();
              },
              child: const Text('Add'),
            ),
          ],
        );
      },
    );
  }
}

// List<DropdownMenuItem<String>> get dropdownItems{
//   List<DropdownMenuItem<String>> menuItems = [
//     const DropdownMenuItem(value: "Relaxation", child: Text("Relaxation")),
//     const DropdownMenuItem(value: "Work", child: Text("Work")),
//     const DropdownMenuItem(value: "Productivity", child: Text("Productivity")),
//     const DropdownMenuItem(value: "Fitness", child: Text("Fitness")),
//   ];
//   return menuItems;
// }
//
// Future<List<DropdownMenuItem<String>>> getDropdownItems() async {
//   List<Categories> categoryItems = await getCategoryList(completedBool: false, catIdList: null);
//
//   List<DropdownMenuItem<String>> menuItems = categoryItems.map((category) {
//     return DropdownMenuItem<String>(
//       value: category.categoryName, // Assuming categoryName is a String
//       child: Text(category.categoryName), // Replace with the appropriate property of Categories
//     );
//   }).toList();
//
//   return menuItems;
// }
