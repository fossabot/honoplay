import moment from "moment";

String.prototype.formatDate = function formatDate(
  customFormat = "DD-MM-YYYY HH:mm"
) {
  return moment(this).format(customFormat);
};

// export default FormatDateTime;
