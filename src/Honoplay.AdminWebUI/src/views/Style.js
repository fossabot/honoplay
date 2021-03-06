import { green } from '@material-ui/core/colors';

const Style = theme => ({
  root: {
    flexGrow: 1,
    padding: 8
  },
  linkStyle: {
    fontSize: 16,
    color: '#673ab7',
    textDecorationLine: 'none',
    '&:active': {
      color: '#673ab7'
    }
  },
  buttonProgress: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 19
  },
  buttonSuccess: {
    textTransform: 'capitalize',
    backgroundColor: green[500],
    boxShadow: '0 .1rem 1rem rgba(0,128,0.25)',
    '&:hover': {
      backgroundColor: green[700],
      textTransform: 'capitalize'
    },
    color: 'white'
  },
  progressStepper: {
    position: 'absolute',
    marginTop: -25,
    marginLeft: 720
  },
  progressModal: {
    position: 'absolute',
    marginTop: -25,
    marginLeft: 800
  },
  buttonProgressUpdate: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 32
  },
  buttonProgressSave: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 30
  },
  questionDiv: {
    borderColor: '#9e9e9e',
    borderStyle: 'solid',
    borderWidth: 1,
    height: 70
  },
  passwordInput: {
    width: '100%'
  },
  bootstrapFormLabel: {
    fontSize: 15,
    color: '#495057'
  },
  tabs: {
    borderBottomColor: '#eeeeee',
    borderBottomStyle: 'solid',
    borderBottomWidth: 1
  },
  tab: {
    textTransform: 'capitalize',
    paddingRight: 150
  },
  center: {
    marginTop: 15
  }
});

export default Style;
