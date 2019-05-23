import terasuProxy from '@omegabigdata/terasu-api-proxy';
terasuProxy.init(3);

String.prototype.translate =  function()  {
    return terasuProxy.translate(this);
};

const LOGIN = "Login".translate();
const EMAIL_ADDRESS  = 'EmailAddress'.translate();
const PASSWORD = 'Password'.translate();
const REMEMBER_ME = 'RememberMe'.translate();
const CHOOSE = 'Choose'.translate();
const ADD = 'Add'.translate();
const TRAINER = 'Trainer'.translate();
const QUESTIONS = 'Questions'.translate();
const TRAINER_SERIES = 'TrainerSeries'.translate();
const TRAINERS = 'Trainers'.translate();
const TRAINEES = 'Trainees'.translate();
const TENANT_INFORMATION = 'TenantInformation'.translate();
const USER_MANAGEMENT = 'UserManagement'.translate();
const REPORTS = 'Reports'.translate();
const SAVE = 'Save'.translate();
const EDIT = 'Edit'.translate();
const REMOVE = 'Remove'.translate();
const NUMBER_OF_ROWS = 'NumberOfRows'.translate();
const SELECTED = 'Selected'.translate();
const BASIC_KNOWLEDGE = 'BasicKnowledge'.translate();
const DEPARTMENT = 'Department'.translate();
const NEW_QUESTION = 'NewQuestion'.translate();
const TENANT_NAME = 'TenantName'.translate();
const TENANT_LOGO = 'TenantLogo'.translate();
const TRAINEE = 'Trainee'.translate();
const EXPORT_FROM_EXCEL = 'ExportFromExcel'.translate();
const WORKING_STATUS = 'WorkingStatus'.translate();
const NAME = 'Name'.translate();
const SURNAME = 'Surname'.translate();
const NATIONAL_IDENTITY_NUMBER = 'NationalIdentityNumber'.translate();
const PHONE_NUMBER = 'PhoneNumber'.translate();
const GENDER = 'Gender'.translate();
const TENANT_DEPARTMENTS = 'TenantDepartments'.translate();


export {
    LOGIN,
    EMAIL_ADDRESS,
    PASSWORD,
    REMEMBER_ME,
    CHOOSE,
    ADD,
    TRAINER,
    QUESTIONS,
    TRAINER_SERIES,
    TRAINERS,
    TRAINEES,
    TENANT_INFORMATION,
    USER_MANAGEMENT,
    REPORTS,
    SAVE,
    EDIT,
    REMOVE,
    NUMBER_OF_ROWS,
    SELECTED,
    BASIC_KNOWLEDGE,
    DEPARTMENT,
    NEW_QUESTION,
    TENANT_NAME,
    TENANT_LOGO,
    TRAINEE,
    EXPORT_FROM_EXCEL,
    WORKING_STATUS,
    NAME,
    SURNAME,
    NATIONAL_IDENTITY_NUMBER,
    PHONE_NUMBER,
    GENDER,
    TENANT_DEPARTMENTS
}