import 'dart:convert';

import 'package:flutter/material.dart';

import 'package:todo_tutorial/pages/current_list.dart';

import '../models/task.dart';
import '../widgets/navbar.dart';
import '../widgets/todo_item.dart';
import 'package:todo_tutorial/api/api_helpers.dart';

class SecondRoute extends StatefulWidget {
  const SecondRoute({super.key});

  //final Task? todo;
  @override
  State<SecondRoute> createState() => SecondRouteState();
}

class SecondRouteState extends State<SecondRoute>{
  bool refreshList = false;

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

  void handleTodoChange(Task todo) {
    toggleTaskCompleted(todo).then((value) => {
      setState(() {
        refreshList = true;
      })
    });
  }

  void deleteTodo(Task todo) {
    deactivateTask(todo).then((value) => {
      setState(() {
        refreshList = true;
      })
    });
  }

  @override
  void initState() {
    super.initState();
    // getTaskList(completedBool: true, catIdList: null);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Completed Route'),
      ),
      body: Column(
          children: <Widget> [

            Expanded(

              child: FutureBuilder(
                // FutureBuilders allows you to create widgets that depend on the completion of a Future and
                // automatically rebuilds your UI when the Future completes or changes its state.
                  future: getTaskList(completedBool: true, catIdList: null),
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
                                    deleteTodo(todo);
                                  });
                                  ScaffoldMessenger.of(context)
                                      .showSnackBar(SnackBar(content: Text('${todo.taskName} dismissed')));
                                } else {
                                  setState(() {
                                    handleTodoChange(todo);
                                  });
                                  ScaffoldMessenger.of(context)
                                      .showSnackBar(SnackBar(content: Text('${todo.taskName} UNCOMPELTED!')));
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
                                color: Colors.orange,
                                child: Align(
                                  alignment: Alignment.centerRight,
                                  child: Padding(
                                    padding: EdgeInsets.all(16.0),
                                    child: Icon(Icons.check_box_outline_blank_rounded, color: Colors.white),
                                  ),
                                ),
                              ),
                              child: TaskItem(
                                  todo: todo,
                                  onTaskChanged: handleTodoChange,
                                  removeTask: deleteTodo)
                          );
                        },
                      );
                    }
                    return Container(); //Handle errors or no items
                  }),
            ),

            ElevatedButton(
              onPressed: () {
                refreshList = true;
                // Navigate back to first route when tapped.
                Navigator.pop(
                    context,
                    MaterialPageRoute(builder: (context) => const TodoList(title: '',))
                );
              },
              child: const Text('Go back!'),
            ),
          ]
      ),
    );
  }

}
