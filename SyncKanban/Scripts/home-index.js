
    var Task = function(id, name) {
        this.id = id;
        this.name = ko.observable(name);
        this.type = "Task";
    };

    var List = function(name, id, tasks) {
        this.tasks = ko.observableArray(tasks);
        this.tasks.id = id;
        this.id = id;
        this.name = name;
        this.type = "List";
    };

    var dashboardViewModel = function(boardId) {
        this.boardId = boardId;
        var self = this;
        this.hub = $.connection.listHub;
        this.lists = ko.observableArray([]);
        this.lastAction = ko.observable();
        this.lastError = ko.observable();

        var lists = this.lists;

        this.init = function() {
            this.hub.server.getAllLists(self.boardId);
        };

        this.hub.client.allLists = function(serverLists) {
            var mappedLists = $.map(serverLists, function(item) {
                return new List(item.Name, item.Id, $.map(item.Tasks, function(childItem) {
                    return new Task(childItem.Id, childItem.Name);
                }));
            });
            lists(mappedLists);
        };

        this.hub.client.syncListMove = function(listId, destinationPosition) {
            //add this move.
        };

        this.hub.client.syncTaskMove = function(taskId, sourceListId, destinationListId, destinationPosition) {
            var sourceTable = ko.utils.arrayFilter(lists(), function(value) { return value.id == sourceListId; })[0];
            var destinationTable = ko.utils.arrayFilter(lists(), function(value) { return value.id == destinationListId; })[0];
            var task = ko.utils.arrayFilter(sourceTable.tasks(), function(value) { return value.id == taskId; })[0];
            sourceTable.tasks.remove(task);
            destinationTable.tasks.splice(destinationPosition, 0, task);
        };

        this.itemDropped = function(arg) {
            if (arg.item.type == 'Task') {
                self.hub.server.movedTask(self.boardId, arg.item.id, arg.sourceParent.id, arg.targetParent.id, arg.targetIndex);
            } else if (arg.item.type == 'List') {
                // need method to save new order of list and sync to other clients.
                self.hub.server.movedList(self.boardId, arg.item.id, arg.targetIndex);
            }
            //self.lastAction("Moved " + arg.item.name() + " from " + arg.sourceParent.id + " (list " + (arg.sourceIndex + 1) + ") to " + arg.targetParent.id + " (list " + (arg.targetIndex + 1) + ")");
        };
    };

    ko.bindingHandlers.flash = {
        init: function(element) {
            $(element).hide();
        },
        update: function(element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            if (value) {
                $(element).stop().hide().text(value).fadeIn(function() {
                    clearTimeout($(element).data("timeout"));
                    $(element).data("timeout", setTimeout(function() {
                        $(element).fadeOut();
                        valueAccessor()(null);
                    }, 5000));
                });
            }
        },
        timeout: null
    };