import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Dialog,
  Paper,
  DialogActions,
  DialogContent,
  DialogTitle,
  Slide,
  List,
  FormGroup,
  FormControlLabel,
  Radio,
  MuiThemeProvider,
  Grid,
  CircularProgress
} from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../Input/InputTextComponent';
import Button from '../Button/ButtonComponent';
import Search from '../Input/SearchInputComponent';

import { connect } from "react-redux";
import {
  fetchWorkingStatusList,
  postWorkingStatus
} from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus";

function Transition(props) {
  return <Slide direction="up" {...props} />;
}

class ModalComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedValue: '',
      loadingCreate: false,
      loadingList: false,
      workingStatusModel: {
        name: ''
      },
      createError: false,
      listError: false,
      workingStatus: []
    }
  }

  handleClick = () => {
    const { postWorkingStatus } = this.props;
    const { workingStatusModel } = this.state;
    postWorkingStatus(workingStatusModel);
    const { fetchWorkingStatusList } = this.props;
    fetchWorkingStatusList(0, 50);
  }

  handleChangeValue = (event) => {
    this.setState({
      selectedValue: event.target.value
    });
  };

  handleChange = (e) => {
    this.setState({
      workingStatusModel: {
        name: e.target.value
      },
      createError: false
    })
  }

  componentDidUpdate(prevProps) {
    const {
      isWorkingStatusCreateLoading,
      workingStatusCreate,
      errorWorkingStatusCreate,
      isWorkingStatusListLoading,
      workingStatusList,
      errorWorkingStatusList
    } = this.props;

    if (!prevProps.isWorkingStatusCreateLoading && isWorkingStatusCreateLoading) {
      this.setState({
        loadingCreate: true
      })
    }
    if (!prevProps.errorWorkingStatusCreate && errorWorkingStatusCreate) {
      this.setState({
        createError: true,
        loadingCreate: false
      })
    }
    if (prevProps.isWorkingStatusCreateLoading && !isWorkingStatusCreateLoading && workingStatusCreate) {
      if (!errorWorkingStatusCreate) {
        this.setState({
          loadingCreate: false,
          createError: false
        });
      }
    }
    if (!prevProps.isWorkingStatusListLoading && isWorkingStatusListLoading) {
      this.setState({
        loadingList: true
      })
    }
    if (!prevProps.errorWorkingStatusList && errorWorkingStatusList) {
      this.setState({
        listError: true,
        loadingList: false
      })
    }
    if (prevProps.isWorkingStatusListLoading && !isWorkingStatusListLoading && workingStatusList) {
      if (!errorWorkingStatusList) {
        this.setState({
          listError: true,
          loadingList: false
        });
      }
    }
  }

  render() {
    const {
      selectedValue,
      loading,
      workingStatusModel,
      createError,
      loadingList
    } = this.state;

    const {
      open,
      handleClickClose,
      modalTitle,
      modalInputName,
      classes,
      data
    } = this.props;

    return (
      <MuiThemeProvider theme={theme}>
        <div>
          <Dialog
            fullWidth
            maxWidth="md"
            open={open}
            TransitionComponent={Transition}
            onClose={handleClickClose}
            aria-labelledby="dialog-slide-title"
          >
            <DialogTitle
              id="dialog-slide-title">
              {modalTitle}
            </DialogTitle>
            <DialogContent>
              <Grid container spacing={24}>
                <Grid item xs={12} sm={12}></Grid>
                <Grid item xs={10} sm={11}>
                  <Input
                    error={createError}
                    onChange={this.handleChange}
                    labelName={modalInputName}
                    inputType="text"
                    name="name"
                    value={workingStatusModel.name}
                  />
                </Grid>
                <Grid item xs={2} sm={1}>
                  <Button
                    buttonColor="secondary"
                    buttonName={translate('Add')}
                    onClick={this.handleClick}
                    disabled={loading}
                  />
                  {loading && (
                    <CircularProgress
                      size={24}
                      disableShrink={true}
                      className={classes.addProgress}
                    />
                  )}
                </Grid>
                <Grid item xs={12} sm={12}></Grid>
                <Grid item xs={12} sm={9}></Grid>
                <Grid item xs={12} sm={3}>
                  <Search />
                </Grid>
                <Grid item xs={12} sm={9}></Grid>
                <Grid item xs={12} sm={12}>
                  {loadingList ?
                    <CircularProgress
                      size={30}
                      disableShrink={true}
                      className={classes.loadingProgress}
                    /> :
                    <Paper
                      className={classes.modalPaper}>
                      {data.map((data, id) => {
                        return (
                          <List
                            dense
                            key={id}
                            className={classes.contextDialog}>
                            <FormGroup row>
                              <FormControlLabel
                                control={
                                  <Radio checked={selectedValue === data.name}
                                    onClick={this.handleChangeValue}
                                    value={data.name}
                                    color='secondary'
                                  />
                                }
                                label={data.name}
                              />
                            </FormGroup>
                          </List>
                        );
                      })}
                    </Paper>
                  }
                </Grid>
              </Grid>
            </DialogContent>
            <DialogActions >
              <Button
                buttonColor="primary"
                buttonName={translate('Save')}
              />
            </DialogActions>
          </Dialog>
        </div>
      </MuiThemeProvider>
    );
  }
}

const mapStateToProps = state => {

  const {
    isWorkingStatusCreateLoading,
    workingStatusCreate,
    errorWorkingStatusCreate
  } = state.workingStatusCreate;
  const {
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  } = state.workingStatusList;

  return {
    isWorkingStatusCreateLoading,
    workingStatusCreate,
    errorWorkingStatusCreate,
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  };
};

const mapDispatchToProps = {
  postWorkingStatus,
  fetchWorkingStatusList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(ModalComponent));