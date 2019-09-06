import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import classNames from "classnames";
import { withStyles } from '@material-ui/core/styles';
import {
    Grid,
    Divider,
    CircularProgress
} from '@material-ui/core';
import Style from '../Style';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';

import { connect } from "react-redux";
import { register } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/AdminUser";

class UserManagement extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            loading: false,
            error: false,
            success: false,
        };
    }

    adminUserModel = {
        name: '',
        surname: '',
        email: '',
        password: ''
    };

    componentDidUpdate(prevProps) {
        const {
            isRegisterLoading,
            registerModel,
            errorRegister
        } = this.props;

        if (!prevProps.isRegisterLoading && isRegisterLoading) {
            this.setState({
                loading: true
            })
        }
        if (!prevProps.errorRegister && errorRegister) {
            this.setState({
                error: true,
                loading: false,
                success: false
            })
        }
        if (prevProps.isRegisterLoading && !isRegisterLoading && registerModel) {
            if (!errorRegister) {
                this.setState({
                    loading: false,
                    error: false,
                    success: true,

                });
                setTimeout(() => {
                    this.setState({ success: false });
                }, 500);
            }
        }
    }

    handleClick = () => {
        const { register } = this.props;
        register(this.adminUserModel);
    }

    render() {
        const {
            loading,
            error,
            success,
        } = this.state;

        const { classes } = this.props;

        const buttonClassname = classNames({
            [classes.buttonSuccess]: success
        });

        return (

            <div className={classes.root}>
                <Grid container spacing={3}>
                    <Typography
                        pageHeader={translate('UserManagement')}
                    />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={12}>
                        <Input
                            error={error}
                            labelName={translate('Name')}
                            inputType="text"
                            onChange={e => {
                                this.adminUserModel.name = e.target.value;
                                this.setState({ error: false });
                            }}
                        />
                        <Input
                            error={error}
                            labelName={translate('Surname')}
                            inputType="text"
                            onChange={e => {
                                this.adminUserModel.surname = e.target.value;
                                this.setState({ error: false });
                            }}
                        />
                        <Input
                            error={error}
                            labelName={translate('EmailAddress')}
                            inputType="text"
                            onChange={e => {
                                this.adminUserModel.email = e.target.value;
                                this.setState({ error: false });
                            }}
                        />
                        <Input
                            error={error}
                            labelName={translate('Password')}
                            inputType="password"
                            onChange={e => {
                                this.adminUserModel.password = e.target.value;
                                this.setState({ error: false });
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} sm={12} />
                    <Grid item xs={12} sm={11} />
                    <Grid item xs={12} sm={1}>
                        <Button
                            className={buttonClassname}
                            buttonColor="primary"
                            buttonName={translate('Save')}
                            onClick={this.handleClick}
                            disabled={loading}
                        />
                        {loading && (
                            <CircularProgress
                                size={24}
                                disableShrink={true}
                                className={classes.buttonProgress}
                            />
                        )}
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <Divider />
                    </Grid>
                </Grid>
            </div>
        );
    }
}


const mapStateToProps = state => {
    const {
        isRegisterLoading,
        registerModel,
        errorRegister
    } = state.register;
    return {
        isRegisterLoading,
        registerModel,
        errorRegister
    };
};

const mapDispatchToProps = {
    register
};


export default connect(
    mapStateToProps,
    mapDispatchToProps
)(withStyles(Style)(UserManagement));