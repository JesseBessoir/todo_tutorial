import 'dart:convert';

import 'package:flutter/material.dart';

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
  // we are including refresh list here so that we can trigger refreshlist whenever we do any of the actions like adding a new task or deleting one.

  Future<List<Task>> getTaskList() async {
    dynamic response = await fetch('Public/GetTaskList', null);
    if (response.statusCode == 200) {
      var decodedList = (jsonDecode(response.body) as List);
      //Parses the string and returns the resulting Json object.
      var mapped = decodedList.map((e) => (Task.fromJson(e))).toList();
      return mapped;
    }
    return Future<List<Task>>.value([]);
  }

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
    getTaskList();
  }

  void _addTodoItem(String taskName) {
    var newTask = Task(taskName: taskName, createdAt: DateTime.now());
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
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: FutureBuilder(
          // FutureBuilders allows you to create widgets that depend on the completion of a Future and
          // automatically rebuilds your UI when the Future completes or changes its state.
          future: getTaskList(),
          //declaring the future that will cause it to rebuild.
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return CircularProgressIndicator();
            }
            if (snapshot.hasData) {
              var todos = snapshot.data!;
              return ListView.builder(
                itemCount: todos?.length,
                itemBuilder: (context, index) {
                  var todo = todos.elementAt(index);
                  return TaskItem(
                      todo: todo,
                      onTaskChanged: _handleTodoChange,
                      removeTask: _deleteTodo);
                },
              );
            }
            return Container(); //Handle errors or no items
          }),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _displayDialog(),
        tooltip: 'Add a Todo',
        child: const Icon(Icons.add),
      ),
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
