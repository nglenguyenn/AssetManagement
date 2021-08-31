export function formatDate(date, isDefault = true) {
  if (date == null) {
    if (isDefault) date = Date.now();
    else return null;
  }
  var d = new Date(date),
    month = "" + (d.getMonth() + 1),
    day = "" + d.getDate(),
    year = d.getFullYear();
  if (month.length < 2) month = "0" + month;
  if (day.length < 2) day = "0" + day;
  return [year, month, day].join("-");
}

export function formatName(firstName, lastName) {
  if (firstName === undefined)
    return lastName;
  if (lastName === undefined)
    return firstName;

  const firstNameWithSpace = firstName.concat(" ");
  return firstNameWithSpace.concat(lastName);
}

export function range(start, end) {
  let length = end - start + 1;
  return Array.from({ length }, (_, idx) => idx + start);
}