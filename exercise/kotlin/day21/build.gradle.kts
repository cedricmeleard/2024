import org.jetbrains.kotlin.gradle.dsl.JvmTarget

plugins {
    kotlin("jvm") version "1.9.21"
    application
}

repositories {
    mavenCentral()
}

dependencies {
    val arrowVersion = "1.2.0"
    implementation("io.arrow-kt:arrow-core:$arrowVersion")

    val kotestVersion = "5.8.0"
    testImplementation("io.kotest:kotest-runner-junit5:$kotestVersion")
    testImplementation("com.approvaltests:approvaltests:18.0.0")
    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

kotlin {
    jvmToolchain(21)

    compilerOptions {
        jvmTarget.set(JvmTarget.JVM_21)
    }
}