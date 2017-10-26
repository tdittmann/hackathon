define(['jquery'], function ($) {
    var repository = function () {
        var self = this;
        jQuery("#loader").hide();
        self.search = {
            go: function (searchInput) {
                var url = "/api/Search";
                //jQuery("#loader").show();
                return self.post(url, { Input: searchInput });
                
            },
          
        }
        
     

        self._beforeSend = function (request) {
            

        }

        self._handleFail = function (data) {

        }

        self._completeSend = function (data) {
            jQuery("#loader").hide();
        }

        self.post = function (url, data, options) {
            options = options || {
                type: 'POST',
                url: url,
                data: data,
                cache: false,
                beforeSend: self._beforeSend,
                success: self._completeSend
                
            };

            return $.ajax(options).fail(self._handleFail);
        }

        self.get = function (url) {
            return $.ajax({
                type: 'GET',
                url: url,
                cache: false,
                beforeSend: self._beforeSend
            }).fail(self._handleFail);
        }
    }

    return new repository();
});

