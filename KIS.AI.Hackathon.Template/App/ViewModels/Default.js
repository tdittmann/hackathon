requirejs(['knockout', 'repository'], function (ko, repository) {
    var viewModel = function (tenant) {
        var self = this;

        self.search = {
            input: ko.observable(''),
            go: function () {
                var searchInput = self.search.input();
               

                repository.search.go(searchInput).done(function (data) {
                    self.results.answers(data.Answers);
                    self.results.related(data.Related);
                    self.results.question(data.Question);
                    jQuery("#loader").hide();

                    jQuery(".answer-read-less-link").click(function () {
                        jQuery(this).hide();
                        jQuery(this).parent().find(".answer-read-more-link").show();
                        jQuery(this).parent().parent().find(".answer-description").toggleClass("answer-container-smaller");
                    });
                    jQuery(".answer-read-more-link").click(function () {
                        jQuery(this).hide();
                        jQuery(this).parent().find(".answer-read-less-link").show();
                        jQuery(this).parent().parent().find(".answer-description").toggleClass("answer-container-smaller");
                    });
                    self.ctor();
                });
            },
            onEvent: function (d, e) {
                if (e.keyCode == 13) {
                    jQuery("#loader").show();
                    self.search.go();
                }
            }

        }

      

        self.results = {
            answers: ko.observableArray(),
            related: ko.observableArray(),
            question: ko.observable()

        };

       

    }

    ko.applyBindings(new viewModel());

   
});
